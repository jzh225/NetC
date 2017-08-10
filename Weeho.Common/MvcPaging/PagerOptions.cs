using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Class PagerOptions.
    /// </summary>
    public class PagerOptions
    {
        /// <summary>
        /// Class DefaultDefaults.
        /// </summary>
        public static class DefaultDefaults
        {
            /// <summary>
            /// The maximum nr of pages
            /// </summary>
            public const int MaxNrOfPages = 10;
            /// <summary>
            /// The display template
            /// </summary>
            public const string DisplayTemplate = null;
            /// <summary>
            /// The always add first page number
            /// </summary>
            public const bool AlwaysAddFirstPageNumber = false;
            /// <summary>
            /// The default page route value key
            /// </summary>
            public const string DefaultPageRouteValueKey = "page";
            /// <summary>
            /// The previous page text
            /// </summary>
            public const string PreviousPageText = "上一页";
            /// <summary>
            /// The previous page title
            /// </summary>
            public const string PreviousPageTitle = "上一页";
            /// <summary>
            /// The next page text
            /// </summary>
            public const string NextPageText = "下一页";
            /// <summary>
            /// The next page title
            /// </summary>
            public const string NextPageTitle = "下一页";
            /// <summary>
            /// The first page text
            /// </summary>
            public const string FirstPageText = "首页";
            /// <summary>
            /// The first page title
            /// </summary>
            public const string FirstPageTitle = "首页";
            /// <summary>
            /// The last page text
            /// </summary>
            public const string LastPageText = "末页";
            /// <summary>
            /// The last page title
            /// </summary>
            public const string LastPageTitle = "末页";
            /// <summary>
            /// The display first and last page
            /// </summary>
            public const bool DisplayFirstAndLastPage = true;
            /// <summary>
            /// The use item count as page count
            /// </summary>
            public const bool UseItemCountAsPageCount = true;
            /// <summary>
            /// The hide previous and next page
            /// </summary>
            public static bool HidePreviousAndNextPage = false;
        }

        /// <summary>
        /// The static Defaults class allows you to set Pager defaults for the entire application.
        /// Set values at application startup.
        /// </summary>
        public static class Defaults
        {
            /// <summary>
            /// The maximum nr of pages
            /// </summary>
            public static int MaxNrOfPages = DefaultDefaults.MaxNrOfPages;
            /// <summary>
            /// The display template
            /// </summary>
            public static string DisplayTemplate = DefaultDefaults.DisplayTemplate;
            /// <summary>
            /// The always add first page number
            /// </summary>
            public static bool AlwaysAddFirstPageNumber = DefaultDefaults.AlwaysAddFirstPageNumber;
            /// <summary>
            /// The default page route value key
            /// </summary>
            public static string DefaultPageRouteValueKey = DefaultDefaults.DefaultPageRouteValueKey;
            /// <summary>
            /// The previous page text
            /// </summary>
            public static string PreviousPageText = DefaultDefaults.PreviousPageText;
            /// <summary>
            /// The previous page title
            /// </summary>
            public static string PreviousPageTitle = DefaultDefaults.PreviousPageTitle;
            /// <summary>
            /// The next page text
            /// </summary>
            public static string NextPageText = DefaultDefaults.NextPageText;
            /// <summary>
            /// The next page title
            /// </summary>
            public static string NextPageTitle = DefaultDefaults.NextPageTitle;
            /// <summary>
            /// The first page text
            /// </summary>
            public static string FirstPageText = DefaultDefaults.FirstPageText;
            /// <summary>
            /// The first page title
            /// </summary>
            public static string FirstPageTitle = DefaultDefaults.FirstPageTitle;
            /// <summary>
            /// The last page text
            /// </summary>
            public static string LastPageText = DefaultDefaults.LastPageText;
            /// <summary>
            /// The last page title
            /// </summary>
            public static string LastPageTitle = DefaultDefaults.LastPageTitle;
            /// <summary>
            /// The display first and last page
            /// </summary>
            public static bool DisplayFirstAndLastPage = DefaultDefaults.DisplayFirstAndLastPage;
            /// <summary>
            /// The use item count as page count
            /// </summary>
            public static bool UseItemCountAsPageCount = DefaultDefaults.UseItemCountAsPageCount;

            /// <summary>
            /// Resets this instance.
            /// </summary>
            public static void Reset()
            {
                MaxNrOfPages = DefaultDefaults.MaxNrOfPages;
                DisplayTemplate = DefaultDefaults.DisplayTemplate;
                AlwaysAddFirstPageNumber = DefaultDefaults.AlwaysAddFirstPageNumber;
                DefaultPageRouteValueKey = DefaultDefaults.DefaultPageRouteValueKey;
                PreviousPageText = DefaultDefaults.PreviousPageText;
                PreviousPageTitle = DefaultDefaults.PreviousPageTitle;
                NextPageText = DefaultDefaults.NextPageText;
                NextPageTitle = DefaultDefaults.NextPageTitle;
                FirstPageText = DefaultDefaults.FirstPageText;
                FirstPageTitle = DefaultDefaults.FirstPageTitle;
                LastPageText = DefaultDefaults.LastPageText;
                LastPageTitle = DefaultDefaults.LastPageTitle;
                DisplayFirstAndLastPage = DefaultDefaults.DisplayFirstAndLastPage;
                UseItemCountAsPageCount = DefaultDefaults.UseItemCountAsPageCount;
            }
        }

        /// <summary>
        /// Gets the route values.
        /// </summary>
        /// <value>The route values.</value>
        public RouteValueDictionary RouteValues { get; internal set; }

        /// <summary>
        /// Gets the display template.
        /// </summary>
        /// <value>The display template.</value>
        public string DisplayTemplate { get; internal set; }

        /// <summary>
        /// Gets the maximum nr of pages.
        /// </summary>
        /// <value>The maximum nr of pages.</value>
        public int MaxNrOfPages { get; internal set; }

        /// <summary>
        /// Gets the ajax options.
        /// </summary>
        /// <value>The ajax options.</value>
        public AjaxOptions AjaxOptions { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether [always add first page number].
        /// </summary>
        /// <value><c>true</c> if [always add first page number]; otherwise, <c>false</c>.</value>
        public bool AlwaysAddFirstPageNumber { get; internal set; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action { get; internal set; }

        /// <summary>
        /// Gets or sets the page route value key.
        /// </summary>
        /// <value>The page route value key.</value>
        public string PageRouteValueKey { get; set; }

        /// <summary>
        /// Gets or sets the previous page text.
        /// </summary>
        /// <value>The previous page text.</value>
        public string PreviousPageText { get; set; }

        /// <summary>
        /// Gets or sets the previous page title.
        /// </summary>
        /// <value>The previous page title.</value>
        public string PreviousPageTitle { get; set; }

        /// <summary>
        /// Gets or sets the next page text.
        /// </summary>
        /// <value>The next page text.</value>
        public string NextPageText { get; set; }

        /// <summary>
        /// Gets or sets the next page title.
        /// </summary>
        /// <value>The next page title.</value>
        public string NextPageTitle { get; set; }

        /// <summary>
        /// Gets or sets the first page text.
        /// </summary>
        /// <value>The first page text.</value>
        public string FirstPageText { get; set; }

        /// <summary>
        /// Gets or sets the first page title.
        /// </summary>
        /// <value>The first page title.</value>
        public string FirstPageTitle { get; set; }

        /// <summary>
        /// Gets or sets the last page text.
        /// </summary>
        /// <value>The last page text.</value>
        public string LastPageText { get; set; }

        /// <summary>
        /// Gets or sets the last page title.
        /// </summary>
        /// <value>The last page title.</value>
        public string LastPageTitle { get; set; }

        /// <summary>
        /// Gets a value indicating whether [display first and last page].
        /// </summary>
        /// <value><c>true</c> if [display first and last page]; otherwise, <c>false</c>.</value>
        public bool DisplayFirstAndLastPage { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether [hide previous and next page].
        /// </summary>
        /// <value><c>true</c> if [hide previous and next page]; otherwise, <c>false</c>.</value>
        public bool HidePreviousAndNextPage { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether [use item count as page count].
        /// </summary>
        /// <value><c>true</c> if [use item count as page count]; otherwise, <c>false</c>.</value>
        public bool UseItemCountAsPageCount { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerOptions"/> class.
        /// </summary>
        public PagerOptions()
        {
            RouteValues = new RouteValueDictionary();
            DisplayTemplate = Defaults.DisplayTemplate;
            MaxNrOfPages = Defaults.MaxNrOfPages;
            AlwaysAddFirstPageNumber = Defaults.AlwaysAddFirstPageNumber;
            PageRouteValueKey = Defaults.DefaultPageRouteValueKey;
            PreviousPageText = DefaultDefaults.PreviousPageText;
            PreviousPageTitle = DefaultDefaults.PreviousPageTitle;
            NextPageText = DefaultDefaults.NextPageText;
            NextPageTitle = DefaultDefaults.NextPageTitle;
            FirstPageText = DefaultDefaults.FirstPageText;
            FirstPageTitle = DefaultDefaults.FirstPageTitle;
            LastPageText = DefaultDefaults.LastPageText;
            LastPageTitle = DefaultDefaults.LastPageTitle;
            DisplayFirstAndLastPage = DefaultDefaults.DisplayFirstAndLastPage;
            UseItemCountAsPageCount = DefaultDefaults.UseItemCountAsPageCount;
            HidePreviousAndNextPage = DefaultDefaults.HidePreviousAndNextPage;
        }
    }
}