// ***********************************************************************
// Assembly         : Infrastructure
// Author           : 
// Created          : 11-14-2016
//
// Last Modified By : 
// Last Modified On : 11-14-2016
// ***********************************************************************
// <copyright file="GifDecoder.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Weeho.Infrastructure.VerifyCode.Gif
{
    /// <summary>
    /// Class GifDecoder.
    /// </summary>
    public class GifDecoder 
	{

        /**
		 * File read status: No errors.
		 */
        /// <summary>
        /// The status ok
        /// </summary>
        public static readonly int STATUS_OK = 0;

        /**
		 * File read status: Error decoding file (may be partially decoded)
		 */
        /// <summary>
        /// The status format error
        /// </summary>
        public static readonly int STATUS_FORMAT_ERROR = 1;

        /**
		 * File read status: Unable to open source.
		 */
        /// <summary>
        /// The status open error
        /// </summary>
        public static readonly int STATUS_OPEN_ERROR = 2;

        /// <summary>
        /// The in stream
        /// </summary>
        protected Stream inStream;
        /// <summary>
        /// The status
        /// </summary>
        protected int status;

        /// <summary>
        /// The width
        /// </summary>
        protected int width; // full image width
                             /// <summary>
                             /// The height
                             /// </summary>
        protected int height; // full image height
                              /// <summary>
                              /// The GCT flag
                              /// </summary>
        protected bool gctFlag; // global color table used
                                /// <summary>
                                /// The GCT size
                                /// </summary>
        protected int gctSize; // size of global color table
                               /// <summary>
                               /// The loop count
                               /// </summary>
        protected int loopCount = 1; // iterations; 0 = repeat forever

        /// <summary>
        /// The GCT
        /// </summary>
        protected int[] gct; // global color table
                             /// <summary>
                             /// The LCT
                             /// </summary>
        protected int[] lct; // local color table
                             /// <summary>
                             /// The act
                             /// </summary>
        protected int[] act; // active color table

        /// <summary>
        /// The bg index
        /// </summary>
        protected int bgIndex; // background color index
                               /// <summary>
                               /// The bg color
                               /// </summary>
        protected int bgColor; // background color
                               /// <summary>
                               /// The last bg color
                               /// </summary>
        protected int lastBgColor; // previous bg color
                                   /// <summary>
                                   /// The pixel aspect
                                   /// </summary>
        protected int pixelAspect; // pixel aspect ratio

        /// <summary>
        /// The LCT flag
        /// </summary>
        protected bool lctFlag; // local color table flag
                                /// <summary>
                                /// The interlace
                                /// </summary>
        protected bool interlace; // interlace flag
                                  /// <summary>
                                  /// The LCT size
                                  /// </summary>
        protected int lctSize; // local color table size

        /// <summary>
        /// The ix
        /// </summary>
        protected int ix, iy, iw, ih; // current image rectangle
                                      /// <summary>
                                      /// The last rect
                                      /// </summary>
        protected Rectangle lastRect; // last image rect
                                      /// <summary>
                                      /// The image
                                      /// </summary>
        protected Image image; // current frame
                               /// <summary>
                               /// The bitmap
                               /// </summary>
        protected Bitmap bitmap;
        /// <summary>
        /// The last image
        /// </summary>
        protected Image lastImage; // previous frame

        /// <summary>
        /// The block
        /// </summary>
        protected byte[] block = new byte[256]; // current data block
                                                /// <summary>
                                                /// The block size
                                                /// </summary>
        protected int blockSize = 0; // block size

        // last graphic control extension info
        /// <summary>
        /// The dispose
        /// </summary>
        protected int dispose = 0;
        // 0=no action; 1=leave in place; 2=restore to bg; 3=restore to prev
        /// <summary>
        /// The last dispose
        /// </summary>
        protected int lastDispose = 0;
        /// <summary>
        /// The transparency
        /// </summary>
        protected bool transparency = false; // use transparent color
                                             /// <summary>
                                             /// The delay
                                             /// </summary>
        protected int delay = 0; // delay in milliseconds
                                 /// <summary>
                                 /// The trans index
                                 /// </summary>
        protected int transIndex; // transparent color index

        /// <summary>
        /// The maximum stack size
        /// </summary>
        protected static readonly int MaxStackSize = 4096;
        // max decoder pixel stack size

        // LZW decoder working arrays
        /// <summary>
        /// The prefix
        /// </summary>
        protected short[] prefix;
        /// <summary>
        /// The suffix
        /// </summary>
        protected byte[] suffix;
        /// <summary>
        /// The pixel stack
        /// </summary>
        protected byte[] pixelStack;
        /// <summary>
        /// The pixels
        /// </summary>
        protected byte[] pixels;

        /// <summary>
        /// The frames
        /// </summary>
        protected ArrayList frames; // frames read from current file
                                    /// <summary>
                                    /// The frame count
                                    /// </summary>
        protected int frameCount;

        /// <summary>
        /// Class GifFrame.
        /// </summary>
        public class GifFrame 
		{
            /// <summary>
            /// Initializes a new instance of the <see cref="GifFrame"/> class.
            /// </summary>
            /// <param name="im">The im.</param>
            /// <param name="del">The delete.</param>
            public GifFrame( Image im, int del) 
			{
				image = im;
				delay = del;
			}
            /// <summary>
            /// The image
            /// </summary>
            public Image image;
            /// <summary>
            /// The delay
            /// </summary>
            public int delay;
		}

        /**
		 * Gets display duration for specified frame.
		 *
		 * @param n int index of frame
		 * @return delay in milliseconds
		 */
        /// <summary>
        /// Gets the delay.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>System.Int32.</returns>
        public int GetDelay(int n) 
		{
			//
			delay = -1;
			if ((n >= 0) && (n < frameCount)) 
			{
				delay = ((GifFrame) frames[n]).delay;
			}
			return delay;
		}

        /**
		 * Gets the number of frames read from file.
		 * @return frame count
		 */
        /// <summary>
        /// Gets the frame count.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetFrameCount() 
		{
			return frameCount;
		}

        /**
		 * Gets the first (or only) image read.
		 *
		 * @return BufferedImage containing first frame, or null if none.
		 */
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>Image.</returns>
        public Image GetImage() 
		{
			return GetFrame(0);
		}

        /**
		 * Gets the "Netscape" iteration count, if any.
		 * A count of 0 means repeat indefinitiely.
		 *
		 * @return iteration count if one was specified, else 1.
		 */
        /// <summary>
        /// Gets the loop count.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetLoopCount() 
		{
			return loopCount;
		}

        /**
		 * Creates new frame image from current data (and previous
		 * frames as specified by their disposition codes).
		 */
        /// <summary>
        /// Gets the pixels.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>System.Int32[].</returns>
        int[] GetPixels( Bitmap bitmap )
		{
			int [] pixels = new int [ 3 * image.Width * image.Height ];
			int count = 0;
			for (int th = 0; th < image.Height; th++)
			{
				for (int tw = 0; tw < image.Width; tw++)
				{
					Color color = bitmap.GetPixel(tw, th);
					pixels[count] = color.R;
					count++;
					pixels[count] = color.G;
					count++;
					pixels[count] = color.B;
					count++;
				}
			}
			return pixels;
		}

        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        void SetPixels( int [] pixels )
		{
			int count = 0;
			for (int th = 0; th < image.Height; th++)
			{
				for (int tw = 0; tw < image.Width; tw++)
				{
					Color color = Color.FromArgb( pixels[count++] );
					bitmap.SetPixel( tw, th, color );
				}
			}
		}

        /// <summary>
        /// Sets the pixels.
        /// </summary>
        protected void SetPixels() 
		{
			// expose destination image's pixels as int array
			//		int[] dest =
			//			(( int ) image.getRaster().getDataBuffer()).getData();
			int[] dest = GetPixels( bitmap );

			// fill in starting image contents based on last image's dispose code
			if (lastDispose > 0) 
			{
				if (lastDispose == 3) 
				{
					// use image before last
					int n = frameCount - 2;
					if (n > 0) 
					{
						lastImage = GetFrame(n - 1);
					} 
					else 
					{
						lastImage = null;
					}
				}

				if (lastImage != null) 
				{
					//				int[] prev =
					//					((DataBufferInt) lastImage.getRaster().getDataBuffer()).getData();
					int[] prev = GetPixels( new Bitmap( lastImage ) );
					Array.Copy(prev, 0, dest, 0, width * height);
					// copy pixels

					if (lastDispose == 2) 
					{
						// fill last image rect area with background color
						Graphics g = Graphics.FromImage( image );
						Color c = Color.Empty;
						if (transparency) 
						{
							c = Color.FromArgb( 0, 0, 0, 0 ); 	// assume background is transparent
						} 
						else 
						{
							c = Color.FromArgb( lastBgColor ) ;
							//						c = new Color(lastBgColor); // use given background color
						}
						Brush brush = new SolidBrush( c );
						g.FillRectangle( brush, lastRect );
						brush.Dispose();
						g.Dispose();
					}
				}
			}

			// copy each source line to the appropriate place in the destination
			int pass = 1;
			int inc = 8;
			int iline = 0;
			for (int i = 0; i < ih; i++) 
			{
				int line = i;
				if (interlace) 
				{
					if (iline >= ih) 
					{
						pass++;
						switch (pass) 
						{
							case 2 :
								iline = 4;
								break;
							case 3 :
								iline = 2;
								inc = 4;
								break;
							case 4 :
								iline = 1;
								inc = 2;
								break;
						}
					}
					line = iline;
					iline += inc;
				}
				line += iy;
				if (line < height) 
				{
					int k = line * width;
					int dx = k + ix; // start of line in dest
					int dlim = dx + iw; // end of dest line
					if ((k + width) < dlim) 
					{
						dlim = k + width; // past dest edge
					}
					int sx = i * iw; // start of line in source
					while (dx < dlim) 
					{
						// map color and insert in destination
						int index = ((int) pixels[sx++]) & 0xff;
						int c = act[index];
						if (c != 0) 
						{
							dest[dx] = c;
						}
						dx++;
					}
				}
			}
			SetPixels( dest );
		}

        /**
		 * Gets the image contents of frame n.
		 *
		 * @return BufferedImage representation of frame, or null if n is invalid.
		 */
        /// <summary>
        /// Gets the frame.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>Image.</returns>
        public Image GetFrame(int n) 
		{
			Image im = null;
			if ((n >= 0) && (n < frameCount)) 
			{
				im = ((GifFrame) frames[n] ).image;
			}
			return im;
		}

        /**
		 * Gets image size.
		 *
		 * @return GIF image dimensions
		 */
        /// <summary>
        /// Gets the size of the frame.
        /// </summary>
        /// <returns>Size.</returns>
        public Size GetFrameSize() 
		{
			return new Size(width, height);
		}

        /**
		 * Reads GIF image from stream
		 *
		 * @param BufferedInputStream containing GIF file.
		 * @return read status code (0 = no errors)
		 */
        /// <summary>
        /// Reads the specified in stream.
        /// </summary>
        /// <param name="inStream">The in stream.</param>
        /// <returns>System.Int32.</returns>
        public int Read( Stream inStream ) 
		{
			Init();
			if ( inStream != null) 
			{
				this.inStream = inStream;
				ReadHeader();
				if (!Error()) 
				{
					ReadContents();
					if (frameCount < 0) 
					{
						status = STATUS_FORMAT_ERROR;
					}
				}
				inStream.Close();
			} 
			else 
			{
				status = STATUS_OPEN_ERROR;
			}
			return status;
		}

        /**
		 * Reads GIF file from specified file/URL source  
		 * (URL assumed if name contains ":/" or "file:")
		 *
		 * @param name String containing source
		 * @return read status code (0 = no errors)
		 */
        /// <summary>
        /// Reads the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32.</returns>
        public int Read(String name) 
		{
			status = STATUS_OK;
			try 
			{
				name = name.Trim().ToLower();
				status = Read( new FileInfo( name ).OpenRead() );
			} 
			catch (IOException e) 
			{
				status = STATUS_OPEN_ERROR;
			}

			return status;
		}

        /**
		 * Decodes LZW image data into pixel array.
		 * Adapted from John Cristy's ImageMagick.
		 */
        /// <summary>
        /// Decodes the image data.
        /// </summary>
        protected void DecodeImageData() 
		{
			int NullCode = -1;
			int npix = iw * ih;
			int available, 
				clear,
				code_mask,
				code_size,
				end_of_information,
				in_code,
				old_code,
				bits,
				code,
				count,
				i,
				datum,
				data_size,
				first,
				top,
				bi,
				pi;

			if ((pixels == null) || (pixels.Length < npix)) 
			{
				pixels = new byte[npix]; // allocate new pixel array
			}
			if (prefix == null) prefix = new short[MaxStackSize];
			if (suffix == null) suffix = new byte[MaxStackSize];
			if (pixelStack == null) pixelStack = new byte[MaxStackSize + 1];

			//  Initialize GIF data stream decoder.

			data_size = Read();
			clear = 1 << data_size;
			end_of_information = clear + 1;
			available = clear + 2;
			old_code = NullCode;
			code_size = data_size + 1;
			code_mask = (1 << code_size) - 1;
			for (code = 0; code < clear; code++) 
			{
				prefix[code] = 0;
				suffix[code] = (byte) code;
			}

			//  Decode GIF pixel stream.

			datum = bits = count = first = top = pi = bi = 0;

			for (i = 0; i < npix;) 
			{
				if (top == 0) 
				{
					if (bits < code_size) 
					{
						//  Load bytes until there are enough bits for a code.
						if (count == 0) 
						{
							// Read a new data block.
							count = ReadBlock();
							if (count <= 0)
								break;
							bi = 0;
						}
						datum += (((int) block[bi]) & 0xff) << bits;
						bits += 8;
						bi++;
						count--;
						continue;
					}

					//  Get the next code.

					code = datum & code_mask;
					datum >>= code_size;
					bits -= code_size;

					//  Interpret the code

					if ((code > available) || (code == end_of_information))
						break;
					if (code == clear) 
					{
						//  Reset decoder.
						code_size = data_size + 1;
						code_mask = (1 << code_size) - 1;
						available = clear + 2;
						old_code = NullCode;
						continue;
					}
					if (old_code == NullCode) 
					{
						pixelStack[top++] = suffix[code];
						old_code = code;
						first = code;
						continue;
					}
					in_code = code;
					if (code == available) 
					{
						pixelStack[top++] = (byte) first;
						code = old_code;
					}
					while (code > clear) 
					{
						pixelStack[top++] = suffix[code];
						code = prefix[code];
					}
					first = ((int) suffix[code]) & 0xff;

					//  Add a new string to the string table,

					if (available >= MaxStackSize)
						break;
					pixelStack[top++] = (byte) first;
					prefix[available] = (short) old_code;
					suffix[available] = (byte) first;
					available++;
					if (((available & code_mask) == 0)
						&& (available < MaxStackSize)) 
					{
						code_size++;
						code_mask += available;
					}
					old_code = in_code;
				}

				//  Pop a pixel off the pixel stack.

				top--;
				pixels[pi++] = pixelStack[top];
				i++;
			}

			for (i = pi; i < npix; i++) 
			{
				pixels[i] = 0; // clear missing pixels
			}

		}

        /**
		 * Returns true if an error was encountered during reading/decoding
		 */
        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool Error() 
		{
			return status != STATUS_OK;
		}

        /**
		 * Initializes or re-initializes reader
		 */
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void Init() 
		{
			status = STATUS_OK;
			frameCount = 0;
			frames = new ArrayList();
			gct = null;
			lct = null;
		}

        /**
		 * Reads a single byte from the input stream.
		 */
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int Read() 
		{
			int curByte = 0;
			try 
			{
				curByte = inStream.ReadByte();
			} 
			catch (IOException e) 
			{
				status = STATUS_FORMAT_ERROR;
			}
			return curByte;
		}

        /**
		 * Reads next variable length block from input.
		 *
		 * @return number of bytes stored in "buffer"
		 */
        /// <summary>
        /// Reads the block.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int ReadBlock() 
		{
			blockSize = Read();
			int n = 0;
			if (blockSize > 0) 
			{
				try 
				{
					int count = 0;
					while (n < blockSize) 
					{
						count = inStream.Read(block, n, blockSize - n);
						if (count == -1) 
							break;
						n += count;
					}
				} 
				catch (IOException e) 
				{
				}

				if (n < blockSize) 
				{
					status = STATUS_FORMAT_ERROR;
				}
			}
			return n;
		}

        /**
		 * Reads color table as 256 RGB integer values
		 *
		 * @param ncolors int number of colors to read
		 * @return int array containing 256 colors (packed ARGB with full alpha)
		 */
        /// <summary>
        /// Reads the color table.
        /// </summary>
        /// <param name="ncolors">The ncolors.</param>
        /// <returns>System.Int32[].</returns>
        protected int[] ReadColorTable(int ncolors) 
		{
			int nbytes = 3 * ncolors;
			int[] tab = null;
			byte[] c = new byte[nbytes];
			int n = 0;
			try 
			{
				n = inStream.Read(c, 0, c.Length );
			} 
			catch (IOException e) 
			{
			}
			if (n < nbytes) 
			{
				status = STATUS_FORMAT_ERROR;
			} 
			else 
			{
				tab = new int[256]; // max size to avoid bounds checks
				int i = 0;
				int j = 0;
				while (i < ncolors) 
				{
					int r = ((int) c[j++]) & 0xff;
					int g = ((int) c[j++]) & 0xff;
					int b = ((int) c[j++]) & 0xff;
					tab[i++] = ( int ) ( 0xff000000 | (r << 16) | (g << 8) | b );
				}
			}
			return tab;
		}

        /**
		 * Main file parser.  Reads GIF content blocks.
		 */
        /// <summary>
        /// Reads the contents.
        /// </summary>
        protected void ReadContents() 
		{
			// read GIF file content blocks
			bool done = false;
			while (!(done || Error())) 
			{
				int code = Read();
				switch (code) 
				{

					case 0x2C : // image separator
						ReadImage();
						break;

					case 0x21 : // extension
						code = Read();
					switch (code) 
					{
						case 0xf9 : // graphics control extension
							ReadGraphicControlExt();
							break;

						case 0xff : // application extension
							ReadBlock();
							String app = "";
							for (int i = 0; i < 11; i++) 
							{
								app += (char) block[i];
							}
							if (app.Equals("NETSCAPE2.0")) 
							{
								ReadNetscapeExt();
							}
							else
								Skip(); // don't care
							break;

						default : // uninteresting extension
							Skip();
							break;
					}
						break;

					case 0x3b : // terminator
						done = true;
						break;

					case 0x00 : // bad byte, but keep going and see what happens
						break;

					default :
						status = STATUS_FORMAT_ERROR;
						break;
				}
			}
		}

        /**
		 * Reads Graphics Control Extension values
		 */
        /// <summary>
        /// Reads the graphic control ext.
        /// </summary>
        protected void ReadGraphicControlExt() 
		{
			Read(); // block size
			int packed = Read(); // packed fields
			dispose = (packed & 0x1c) >> 2; // disposal method
			if (dispose == 0) 
			{
				dispose = 1; // elect to keep old image if discretionary
			}
			transparency = (packed & 1) != 0;
			delay = ReadShort() * 10; // delay in milliseconds
			transIndex = Read(); // transparent color index
			Read(); // block terminator
		}

        /**
		 * Reads GIF file header information.
		 */
        /// <summary>
        /// Reads the header.
        /// </summary>
        protected void ReadHeader() 
		{
			String id = "";
			for (int i = 0; i < 6; i++) 
			{
				id += (char) Read();
			}
			if (!id.StartsWith("GIF")) 
			{
				status = STATUS_FORMAT_ERROR;
				return;
			}

			ReadLSD();
			if (gctFlag && !Error()) 
			{
				gct = ReadColorTable(gctSize);
				bgColor = gct[bgIndex];
			}
		}

        /**
		 * Reads next frame image
		 */
        /// <summary>
        /// Reads the image.
        /// </summary>
        protected void ReadImage() 
		{
			ix = ReadShort(); // (sub)image position & size
			iy = ReadShort();
			iw = ReadShort();
			ih = ReadShort();

			int packed = Read();
			lctFlag = (packed & 0x80) != 0; // 1 - local color table flag
			interlace = (packed & 0x40) != 0; // 2 - interlace flag
			// 3 - sort flag
			// 4-5 - reserved
			lctSize = 2 << (packed & 7); // 6-8 - local color table size

			if (lctFlag) 
			{
				lct = ReadColorTable(lctSize); // read table
				act = lct; // make local table active
			} 
			else 
			{
				act = gct; // make global table active
				if (bgIndex == transIndex)
					bgColor = 0;
			}
			int save = 0;
			if (transparency) 
			{
				save = act[transIndex];
				act[transIndex] = 0; // set transparent color if specified
			}

			if (act == null) 
			{
				status = STATUS_FORMAT_ERROR; // no color table defined
			}

			if (Error()) return;

			DecodeImageData(); // decode pixel data
			Skip();

			if (Error()) return;

			frameCount++;

			// create new image to receive frame data
			//		image =
			//			new BufferedImage(width, height, BufferedImage.TYPE_INT_ARGB_PRE);

			bitmap = new Bitmap( width, height );
			image = bitmap;
			SetPixels(); // transfer pixel data to image

			frames.Add(new GifFrame(bitmap, delay)); // add image to frame list

			if (transparency) 
			{
				act[transIndex] = save;
			}
			ResetFrame();

		}

        /**
		 * Reads Logical Screen Descriptor
		 */
        /// <summary>
        /// Reads the LSD.
        /// </summary>
        protected void ReadLSD() 
		{

			// logical screen size
			width = ReadShort();
			height = ReadShort();

			// packed fields
			int packed = Read();
			gctFlag = (packed & 0x80) != 0; // 1   : global color table flag
			// 2-4 : color resolution
			// 5   : gct sort flag
			gctSize = 2 << (packed & 7); // 6-8 : gct size

			bgIndex = Read(); // background color index
			pixelAspect = Read(); // pixel aspect ratio
		}

        /**
		 * Reads Netscape extenstion to obtain iteration count
		 */
        /// <summary>
        /// Reads the netscape ext.
        /// </summary>
        protected void ReadNetscapeExt() 
		{
			do 
			{
				ReadBlock();
				if (block[0] == 1) 
				{
					// loop count sub-block
					int b1 = ((int) block[1]) & 0xff;
					int b2 = ((int) block[2]) & 0xff;
					loopCount = (b2 << 8) | b1;
				}
			} while ((blockSize > 0) && !Error());
		}

        /**
		 * Reads next 16-bit value, LSB first
		 */
        /// <summary>
        /// Reads the short.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int ReadShort() 
		{
			// read 16-bit value, LSB first
			return Read() | (Read() << 8);
		}

        /**
		 * Resets frame state for reading next image.
		 */
        /// <summary>
        /// Resets the frame.
        /// </summary>
        protected void ResetFrame() 
		{
			lastDispose = dispose;
			lastRect = new Rectangle(ix, iy, iw, ih);
			lastImage = image;
			lastBgColor = bgColor;
			//		int dispose = 0;
			lct = null;
		}

        /**
		 * Skips variable length blocks up to and including
		 * next zero length block.
		 */
        /// <summary>
        /// Skips this instance.
        /// </summary>
        protected void Skip() 
		{
			do 
			{
				ReadBlock();
			} while ((blockSize > 0) && !Error());
		}
	}
}
