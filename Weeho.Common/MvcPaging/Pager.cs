using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Class Pager.
    /// </summary>
    /// <seealso cref="System.Web.IHtmlString" />
    public class Pager : IHtmlString
    {
        /// <summary>
        /// The HTML helper
        /// </summary>
        private readonly HtmlHelper htmlHelper;
        /// <summary>
        /// The page size
        /// </summary>
        private readonly int pageSize;
        /// <summary>
        /// The current page
        /// </summary>
        private readonly int currentPage;
        /// <summary>
        /// The total item count
        /// </summary>
        private int totalItemCount;
        /// <summary>
        /// The pager options
        /// </summary>
        protected readonly PagerOptions pagerOptions;
        /// <summary>
        /// The generate page action
        /// </summary>
        private Func<int, string> GeneratePageAction;
        /// <summary>
        /// Initializes a new instance of the <see cref="Pager"/> class.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        public Pager(HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
        {
            this.htmlHelper = htmlHelper;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.pagerOptions = new PagerOptions();
            this.GeneratePageAction = (pageNumber) => GeneratePageUrl2(pageNumber);
        }

        /// <summary>
        /// Optionses the specified build options.
        /// </summary>
        /// <param name="buildOptions">The build options.</param>
        /// <returns>Pager.</returns>
        public Pager Options(Action<PagerOptionsBuilder> buildOptions)
        {
            buildOptions(new PagerOptionsBuilder(this.pagerOptions));
            return this;
        }
        /// <summary>
        /// Pages the URL action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Pager.</returns>
        public Pager PageUrlAction(Func<int, string> action)
        {
            this.GeneratePageAction = action;
            return this;
        }
        /// <summary>
        /// Builds the pagination model.
        /// </summary>
        /// <param name="generateUrl">The generate URL.</param>
        /// <returns>PaginationModel.</returns>
        public virtual PaginationModel BuildPaginationModel(Func<int, string> generateUrl)
        {
            int pageCount;
            if (this.pagerOptions.UseItemCountAsPageCount)
            {
                // Set page count directly from total item count instead of calculating. Then calculate totalItemCount based on pageCount and pageSize;
                pageCount = this.totalItemCount;
                this.totalItemCount = pageCount * this.pageSize;
            }
            else
            {
                pageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            }

            var model = new PaginationModel { PageSize = this.pageSize, CurrentPage = this.currentPage, TotalItemCount = this.totalItemCount, PageCount = pageCount };

            // First page
            if (this.pagerOptions.DisplayFirstAndLastPage)
            {
                model.PaginationLinks.Add(new PaginationLink { Active = (currentPage > 1 ? true : false), DisplayText = this.pagerOptions.FirstPageText, DisplayTitle = this.pagerOptions.FirstPageTitle, PageIndex = 1, Url = generateUrl(1) });
            }

            // Previous page
            if (!this.pagerOptions.HidePreviousAndNextPage)
            {
                var previousPageText = this.pagerOptions.PreviousPageText;
                model.PaginationLinks.Add(currentPage > 1 ? new PaginationLink { Active = true, DisplayText = previousPageText, DisplayTitle = this.pagerOptions.PreviousPageTitle, PageIndex = currentPage - 1, Url = generateUrl(currentPage - 1) } : new PaginationLink { Active = false, DisplayText = previousPageText });
            }

            var start = 1;
            var end = pageCount;
            var nrOfPagesToDisplay = this.pagerOptions.MaxNrOfPages;

            if (pageCount > nrOfPagesToDisplay)
            {
                var middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                var below = (currentPage - middle);
                var above = (currentPage + middle);

                if (below < 2)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 2))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay + 1);
                }

                start = below;
                end = above;
            }

            if (start > 1)
            {
                model.PaginationLinks.Add(new PaginationLink { Active = true, PageIndex = 1, DisplayText = "1", Url = generateUrl(1) });
                if (start > 3)
                {
                    model.PaginationLinks.Add(new PaginationLink { Active = true, PageIndex = 2, DisplayText = "2", Url = generateUrl(2) });
                }
                if (start > 2)
                {
                    model.PaginationLinks.Add(new PaginationLink { Active = false, DisplayText = "...", IsSpacer = true });
                }
            }

            for (var i = start; i <= end; i++)
            {
                if (i == currentPage || (currentPage <= 0 && i == 1))
                {
                    model.PaginationLinks.Add(new PaginationLink { Active = true, PageIndex = i, IsCurrent = true, DisplayText = i.ToString() });
                }
                else
                {
                    model.PaginationLinks.Add(new PaginationLink { Active = true, PageIndex = i, DisplayText = i.ToString(), Url = generateUrl(i) });
                }
            }

            if (end < pageCount)
            {
                if (end < pageCount - 1)
                {
                    model.PaginationLinks.Add(new PaginationLink { Active = false, DisplayText = "...", IsSpacer = true });
                }
                if (pageCount - 2 > end)
                {
                    model.PaginationLinks.Add(new PaginationLink { Active = true, PageIndex = pageCount - 1, DisplayText = (pageCount - 1).ToString(), Url = generateUrl(pageCount - 1) });
                }
            }

            // Next page
            if (!this.pagerOptions.HidePreviousAndNextPage)
            {
                var nextPageText = this.pagerOptions.NextPageText;
                model.PaginationLinks.Add(currentPage < pageCount ?
                    new PaginationLink { Active = true, PageIndex = currentPage + 1, DisplayText = nextPageText, DisplayTitle = this.pagerOptions.NextPageTitle, Url = generateUrl(currentPage + 1) } :
                    new PaginationLink { Active = false, DisplayText = nextPageText });
            }

            // Last page
            if (this.pagerOptions.DisplayFirstAndLastPage)
            {
                model.PaginationLinks.Add(new PaginationLink { Active = (currentPage < pageCount ? true : false), DisplayText = this.pagerOptions.LastPageText, DisplayTitle = this.pagerOptions.LastPageTitle, PageIndex = pageCount, Url = generateUrl(pageCount) });
            }

            // AjaxOptions
            if (pagerOptions.AjaxOptions != null)
            {
                model.AjaxOptions = pagerOptions.AjaxOptions;
            }

            model.Options = pagerOptions;
            return model;
        }

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>An HTML-encoded string.</returns>
        public virtual string ToHtmlString()
        {
            var model = BuildPaginationModel(GeneratePageAction);

            if (this.totalItemCount == 0)
            {
                return string.Empty;
            }

            if (!String.IsNullOrEmpty(this.pagerOptions.DisplayTemplate))
            {
                var templatePath = string.Format("DisplayTemplates/{0}", this.pagerOptions.DisplayTemplate);
                return htmlHelper.Partial(templatePath, model).ToHtmlString();
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append("<ul class=\"pagination\">");
                foreach (var paginationLink in model.PaginationLinks)
                {
                    if (paginationLink.Active)
                    {
                        if (paginationLink.IsCurrent)
                        {
                            sb.AppendFormat("<li class=\"disabled\"><a href=\"javascript:;\">{0}</a></li>", paginationLink.DisplayText);
                        }
                        else if (!paginationLink.PageIndex.HasValue)
                        {
                            sb.AppendFormat("<li class=\"disabled\"><a href=\"javascript:;\">{0}</a></li>",paginationLink.DisplayText);
                        }
                        else
                        {
                            var linkBuilder = new StringBuilder("<li><a");

                            if (pagerOptions.AjaxOptions != null)
                                foreach (var ajaxOption in pagerOptions.AjaxOptions.ToUnobtrusiveHtmlAttributes())
                                    linkBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);

                            linkBuilder.AppendFormat(" href=\"{0}\" data-page=\"{3}\" title=\"{1}\">{2}</a></li>",
                                paginationLink.Url, paginationLink.DisplayTitle, paginationLink.DisplayText, paginationLink.PageIndex);

                            sb.Append(linkBuilder.ToString());
                        }
                    }
                    else
                    {
                        if (!paginationLink.IsSpacer)
                        {
                            sb.AppendFormat("<li class=\"disabled\"><a href=\"javascript:;\">{0}</a></li>", paginationLink.DisplayText);
                        }
                        else
                        {
                            sb.AppendFormat("<li><a href=\"javascript:;\"  class=\"spacer\">{0}</a></li>", paginationLink.DisplayText);
                        }
                    }
                }
                sb.Append("</ul>");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Generates the page URL.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>System.String.</returns>
        protected virtual string GeneratePageUrl(int pageNumber)
        {
            var viewContext = this.htmlHelper.ViewContext;
            var routeDataValues = viewContext.RequestContext.RouteData.Values;
            RouteValueDictionary pageLinkValueDictionary;

            // Avoid canonical errors when pageNumber is equal to 1.
            if (pageNumber == 1 && !this.pagerOptions.AlwaysAddFirstPageNumber)
            {
                pageLinkValueDictionary = new RouteValueDictionary(this.pagerOptions.RouteValues);

                if (routeDataValues.ContainsKey(this.pagerOptions.PageRouteValueKey))
                {
                    routeDataValues.Remove(this.pagerOptions.PageRouteValueKey);
                }
            }
            else
            {
                pageLinkValueDictionary = new RouteValueDictionary(this.pagerOptions.RouteValues) { { this.pagerOptions.PageRouteValueKey, pageNumber } };
            }

            // To be sure we get the right route, ensure the controller and action are specified.
            if (!pageLinkValueDictionary.ContainsKey("controller") && routeDataValues.ContainsKey("controller"))
            {
                pageLinkValueDictionary.Add("controller", routeDataValues["controller"]);
            }

            if (!pageLinkValueDictionary.ContainsKey("action") && routeDataValues.ContainsKey("action"))
            {
                pageLinkValueDictionary.Add("action", routeDataValues["action"]);
            }

            // Fix the dictionary if there are arrays in it.
            pageLinkValueDictionary = pageLinkValueDictionary.FixListRouteDataValues();

            // 'Render' virtual path.
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLinkValueDictionary);

            return virtualPathForArea == null ? null : virtualPathForArea.VirtualPath;
        }

        /// <summary>
        /// Generates the page url2.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>System.String.</returns>
        protected virtual string GeneratePageUrl2(int pageNumber)
        {
            var request = this.htmlHelper.ViewContext.RequestContext.HttpContext.Request;
            string url = request.RawUrl;
            var separateURL = url.Split('?');
            NameValueCollection query = null;
            if (separateURL.Length > 1)
            {
                query = System.Web.HttpUtility.ParseQueryString(separateURL[1]);
                query.Remove(this.pagerOptions.PageRouteValueKey);
            }
            else
            {
                query = new NameValueCollection();
            }
            if (pageNumber > 1)
            {
                query.Add(this.pagerOptions.PageRouteValueKey, pageNumber.ToString());
            }
            if (query.AllKeys.Length == 0)
            {
                return separateURL[0];
            }
            var queryUrl = string.Empty;
            foreach (var item in query.AllKeys)
            {
                queryUrl += string.Format("{0}={1}&",item,query[item] );
            }
            return separateURL[0] += "?" + queryUrl.TrimEnd('&');
        }
    }

    /// <summary>
    /// Class Pager.
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    /// <seealso cref="Common.MvcPaging.Pager" />
    public class Pager<TModel> : Pager
    {
        /// <summary>
        /// The HTML helper
        /// </summary>
        private HtmlHelper<TModel> htmlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pager{TModel}"/> class.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        public Pager(HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount)
            : base(htmlHelper, pageSize, currentPage, totalItemCount)
        {
            this.htmlHelper = htmlHelper;
        }

        /// <summary>
        /// Optionses the specified build options.
        /// </summary>
        /// <param name="buildOptions">The build options.</param>
        /// <returns>Pager&lt;TModel&gt;.</returns>
        public Pager<TModel> Options(Action<PagerOptionsBuilder<TModel>> buildOptions)
        {
            buildOptions(new PagerOptionsBuilder<TModel>(this.pagerOptions, htmlHelper));
            return this;
        }
    }
}