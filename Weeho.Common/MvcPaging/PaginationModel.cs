using System.Collections.Generic;
using System.Web.Mvc.Ajax;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Class PaginationModel.
    /// </summary>
    public class PaginationModel
    {
        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; internal set; }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        /// <value>The page count.</value>
        public int PageCount { get; internal set; }

        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public int TotalItemCount { get; internal set; }

        /// <summary>
        /// Gets the pagination links.
        /// </summary>
        /// <value>The pagination links.</value>
        public IList<PaginationLink> PaginationLinks { get; private set; }

        /// <summary>
        /// Gets the ajax options.
        /// </summary>
        /// <value>The ajax options.</value>
        public AjaxOptions AjaxOptions { get; internal set; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public PagerOptions Options { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationModel"/> class.
        /// </summary>
        public PaginationModel()
        {
            PaginationLinks = new List<PaginationLink>();
            AjaxOptions = null;
            Options = null;
        }
    }

    /// <summary>
    /// Class PaginationLink.
    /// </summary>
    public class PaginationLink
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PaginationLink"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value><c>true</c> if this instance is current; otherwise, <c>false</c>.</value>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int? PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>The display text.</value>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets the display title.
        /// </summary>
        /// <value>The display title.</value>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is spacer.
        /// </summary>
        /// <value><c>true</c> if this instance is spacer; otherwise, <c>false</c>.</value>
        public bool IsSpacer { get; set; }
    }
}