using System;
using System.Collections.Generic;
using System.Linq;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Class PagedList.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    /// <seealso cref="Common.MvcPaging.IPagedList{T}" />
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        public PagedList(IEnumerable<T> source, int index, int pageSize, int? totalCount = null)
            : this(source.AsQueryable(), index, pageSize, totalCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index - Value can not be below 0.
        /// or
        /// pageSize - Value can not be less than 1.
        /// </exception>
        public PagedList(IQueryable<T> source, int index, int pageSize, int? totalCount = null)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Value can not be below 0.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", "Value can not be less than 1.");

            if (source == null)
                source = new List<T>().AsQueryable();

            var realTotalCount = source.Count();

            PageSize = pageSize;
            PageIndex = index;
            TotalItemCount = totalCount.HasValue ? totalCount.Value : realTotalCount;
            PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;

            HasPreviousPage = (PageIndex > 0);
            HasNextPage = (PageIndex < (PageCount - 1));
            IsFirstPage = (PageIndex <= 0);
            IsLastPage = (PageIndex >= (PageCount - 1));

            ItemStart = PageIndex * PageSize + 1;
            ItemEnd = Math.Min(PageIndex * PageSize + PageSize, TotalItemCount);

            if (TotalItemCount <= 0)
                return;

            var realTotalPages = (int)Math.Ceiling(realTotalCount / (double)PageSize);

            if (realTotalCount < TotalItemCount && realTotalPages <= PageIndex)
                AddRange(source.Skip((realTotalPages - 1) * PageSize).Take(PageSize));
            else
                AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        #region IPagedList Members

        /// <summary>
        /// Gets the page count.
        /// </summary>
        /// <value>The page count.</value>
        public int PageCount { get; private set; }
        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public int TotalItemCount { get; private set; }
        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; private set; }
        /// <summary>
        /// Gets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int PageNumber { get { return PageIndex + 1; } }
        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value><c>true</c> if this instance has previous page; otherwise, <c>false</c>.</value>
        public bool HasPreviousPage { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value><c>true</c> if this instance has next page; otherwise, <c>false</c>.</value>
        public bool HasNextPage { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is first page.
        /// </summary>
        /// <value><c>true</c> if this instance is first page; otherwise, <c>false</c>.</value>
        public bool IsFirstPage { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is last page.
        /// </summary>
        /// <value><c>true</c> if this instance is last page; otherwise, <c>false</c>.</value>
        public bool IsLastPage { get; private set; }
        /// <summary>
        /// Gets the item start.
        /// </summary>
        /// <value>The item start.</value>
        public int ItemStart { get; private set; }
        /// <summary>
        /// Gets the item end.
        /// </summary>
        /// <value>The item end.</value>
        public int ItemEnd { get; private set; }

        #endregion
    }
}