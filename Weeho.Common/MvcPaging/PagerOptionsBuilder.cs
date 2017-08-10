using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Pager options builder class. Enables a fluent interface for adding options to the pager.
    /// </summary>
    public class PagerOptionsBuilder
    {
        /// <summary>
        /// The pager options
        /// </summary>
        protected PagerOptions pagerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerOptionsBuilder"/> class.
        /// </summary>
        /// <param name="pagerOptions">The pager options.</param>
        public PagerOptionsBuilder(PagerOptions pagerOptions)
        {
            this.pagerOptions = pagerOptions;
        }

        /// <summary>
        /// Set the action name for the pager links. Note that we're always using the current controller.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        /// <exception cref="System.ArgumentException">The valuesDictionary already contains an action. - action</exception>
        public PagerOptionsBuilder Action(string action)
        {
            if (action != null)
            {
                if (pagerOptions.RouteValues.ContainsKey("action"))
                {
                    throw new ArgumentException("The valuesDictionary already contains an action.", "action");
                }
                pagerOptions.RouteValues.Add("action", action);
                pagerOptions.Action = action;
            }
            return this;
        }

        /// <summary>
        /// Add a custom route value parameter for the pager links.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder AddRouteValue(string name, object value)
        {
            pagerOptions.RouteValues[name] = value;
            return this;
        }

        /// <summary>
        /// Set the text for previous page navigation.
        /// </summary>
        /// <param name="previousPageText">The previous page text.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetPreviousPageText(string previousPageText)
        {
            pagerOptions.PreviousPageText = previousPageText;
            return this;
        }

        /// <summary>
        /// Set the title for previous page navigation.
        /// </summary>
        /// <param name="previousPageTitle">The previous page title.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetPreviousPageTitle(string previousPageTitle)
        {
            pagerOptions.PreviousPageTitle = previousPageTitle;
            return this;
        }

        /// <summary>
        /// Set the text for next page navigation.
        /// </summary>
        /// <param name="nextPageText">The next page text.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetNextPageText(string nextPageText)
        {
            pagerOptions.NextPageText = nextPageText;
            return this;
        }

        /// <summary>
        /// Set the title for next page navigation.
        /// </summary>
        /// <param name="nextPageTitle">The next page title.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetNextPageTitle(string nextPageTitle)
        {
            pagerOptions.NextPageTitle = nextPageTitle;
            return this;
        }

        /// <summary>
        /// Set the text for first page navigation.
        /// </summary>
        /// <param name="firstPageText">The first page text.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetFirstPageText(string firstPageText)
        {
            pagerOptions.FirstPageText = firstPageText;
            return this;
        }

        /// <summary>
        /// Set the title for first page navigation.
        /// </summary>
        /// <param name="firstPageTitle">The first page title.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetFirstPageTitle(string firstPageTitle)
        {
            pagerOptions.FirstPageTitle = firstPageTitle;
            return this;
        }

        /// <summary>
        /// Set the text for last page navigation.
        /// </summary>
        /// <param name="lastPageText">The last page text.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetLastPageText(string lastPageText)
        {
            pagerOptions.LastPageText = lastPageText;
            return this;
        }

        /// <summary>
        /// Set the title for last page navigation.
        /// </summary>
        /// <param name="lastPageTitle">The last page title.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder SetLastPageTitle(string lastPageTitle)
        {
            pagerOptions.LastPageTitle = lastPageTitle;
            return this;
        }

        /// <summary>
        /// Displays first and last navigation pages.
        /// </summary>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder DisplayFirstAndLastPage()
        {
            pagerOptions.DisplayFirstAndLastPage = true;
            return this;
        }
        public PagerOptionsBuilder HideFirstAndLastPage()
        {
            pagerOptions.DisplayFirstAndLastPage = false;
            return this;
        }

        /// <summary>
        /// Hides the previous and next page.
        /// </summary>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder HidePreviousAndNextPage()
        {
            pagerOptions.HidePreviousAndNextPage = true;
            return this;
        }

        /// <summary>
        /// Set custom route value parameters for the pager links.
        /// </summary>
        /// <param name="routeValues">The route values.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder RouteValues(object routeValues)
		{
			RouteValues(new RouteValueDictionary(routeValues));
			return this;
		}

        /// <summary>
        /// Set custom route value parameters for the pager links.
        /// </summary>
        /// <param name="routeValues">The route values.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        /// <exception cref="System.ArgumentException">routeValues may not be null - routeValues</exception>
        public PagerOptionsBuilder RouteValues(RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                throw new ArgumentException("routeValues may not be null", "routeValues");
            }
            this.pagerOptions.RouteValues = routeValues;
            if (!string.IsNullOrWhiteSpace(pagerOptions.Action) && !pagerOptions.RouteValues.ContainsKey("action"))
            {
                pagerOptions.RouteValues.Add("action", pagerOptions.Action);
            }
            return this;
        }

        /// <summary>
        /// Set the name of the DisplayTemplate view to use for rendering.
        /// </summary>
        /// <param name="displayTemplate">The display template.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        /// <remarks>The view must have a model of IEnumerable&lt;PaginationModel&gt;</remarks>
        public PagerOptionsBuilder DisplayTemplate(string displayTemplate)
        {
            this.pagerOptions.DisplayTemplate = displayTemplate;
            return this;
        }

        /// <summary>
        /// Set the maximum number of pages to show. The default is 10.
        /// </summary>
        /// <param name="maxNrOfPages">The maximum nr of pages.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder MaxNrOfPages(int maxNrOfPages)
        {
            this.pagerOptions.MaxNrOfPages = maxNrOfPages;
            return this;
        }

        /// <summary>
        /// Always add the page number to the generated link for the first page.
        /// </summary>
        /// <returns>PagerOptionsBuilder.</returns>
        /// <remarks>By default we don't add the page number for page 1 because it results in canonical links.
        /// Use this option to override this behaviour.</remarks>
        public PagerOptionsBuilder AlwaysAddFirstPageNumber()
        {
            this.pagerOptions.AlwaysAddFirstPageNumber = true;
            return this;
        }

        /// <summary>
        /// Set the page routeValue key for pagination links
        /// </summary>
        /// <param name="pageRouteValueKey">The page route value key.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        /// <exception cref="System.ArgumentException">pageRouteValueKey may not be null - pageRouteValueKey</exception>
        public PagerOptionsBuilder PageRouteValueKey(string pageRouteValueKey)
        {
            if (pageRouteValueKey == null)
            {
                throw new ArgumentException("pageRouteValueKey may not be null", "pageRouteValueKey");
            }
            this.pagerOptions.PageRouteValueKey = pageRouteValueKey;
            return this;
        }

        /// <summary>
        /// Indicate that the total item count means total page count. This option is for scenario's
        /// where certain backends don't return the number of total items, but the number of pages.
        /// </summary>
        /// <returns>PagerOptionsBuilder.</returns>
        public PagerOptionsBuilder UseItemCountAsPageCount()
        {
            this.pagerOptions.UseItemCountAsPageCount = true;
            return this;
        }

        /// <summary>
        /// Set the AjaxOptions.
        /// </summary>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <returns>PagerOptionsBuilder.</returns>
        internal PagerOptionsBuilder AjaxOptions(AjaxOptions ajaxOptions)
        {
            this.pagerOptions.AjaxOptions = ajaxOptions;
            return this;
        }
    }

    /// <summary>
    /// Class PagerOptionsBuilder.
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    /// <seealso cref="Common.MvcPaging.PagerOptionsBuilder" />
    public class PagerOptionsBuilder<TModel> : PagerOptionsBuilder
    {
        /// <summary>
        /// The HTML helper
        /// </summary>
        private readonly HtmlHelper<TModel> htmlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerOptionsBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="pagerOptions">The pager options.</param>
        /// <param name="htmlHelper">The HTML helper.</param>
        public PagerOptionsBuilder(PagerOptions pagerOptions, HtmlHelper<TModel> htmlHelper)
            : base(pagerOptions)
        {
            this.htmlHelper = htmlHelper;
        }

        /// <summary>
        /// Adds a strongly typed route value parameter based on the current model.
        /// </summary>
        /// <typeparam name="TProperty">The type of the t property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        /// <example>AddRouteValueFor(m =&gt; m.SearchQuery)</example>
        public PagerOptionsBuilder<TModel> AddRouteValueFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            AddRouteValue(name, metadata.Model);

            return this;
        }

        /// <summary>
        /// Set the action name for the pager links. Note that we're always using the current controller.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> Action(string action)
        {
            base.Action(action);
            return this;
        }

        /// <summary>
        /// Add a custom route value parameter for the pager links.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> AddRouteValue(string name, object value)
        {
            base.AddRouteValue(name, value);
            return this;
        }

        /// <summary>
        /// Set custom route value parameters for the pager links.
        /// </summary>
        /// <param name="routeValues">The route values.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> RouteValues(object routeValues)
        {
            base.RouteValues(routeValues);
            return this;
        }

        /// <summary>
        /// Set custom route value parameters for the pager links.
        /// </summary>
        /// <param name="routeValues">The route values.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> RouteValues(RouteValueDictionary routeValues)
        {
            base.RouteValues(routeValues);
            return this;
        }

        /// <summary>
        /// Set the name of the DisplayTemplate view to use for rendering.
        /// </summary>
        /// <param name="displayTemplate">The display template.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        /// <remarks>The view must have a model of IEnumerable&lt;PaginationModel&gt;</remarks>
        public new PagerOptionsBuilder<TModel> DisplayTemplate(string displayTemplate)
        {
            base.DisplayTemplate(displayTemplate);
            return this;
        }

        /// <summary>
        /// Set the maximum number of pages to show. The default is 10.
        /// </summary>
        /// <param name="maxNrOfPages">The maximum nr of pages.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> MaxNrOfPages(int maxNrOfPages)
        {
            base.MaxNrOfPages(maxNrOfPages);
            return this;
        }

        /// <summary>
        /// Always add the page number to the generated link for the first page.
        /// </summary>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        /// <remarks>By default we don't add the page number for page 1 because it results in canonical links.
        /// Use this option to override this behaviour.</remarks>
        public new PagerOptionsBuilder<TModel> AlwaysAddFirstPageNumber()
        {
            base.AlwaysAddFirstPageNumber();
            return this;
        }

        /// <summary>
        /// Set the page routeValue key for pagination links
        /// </summary>
        /// <param name="pageRouteValueKey">The page route value key.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        /// <exception cref="System.ArgumentException">pageRouteValueKey may not be null - pageRouteValueKey</exception>
        public new PagerOptionsBuilder<TModel> PageRouteValueKey(string pageRouteValueKey)
        {
            if (pageRouteValueKey == null)
            {
                throw new ArgumentException("pageRouteValueKey may not be null", "pageRouteValueKey");
            }
            this.pagerOptions.PageRouteValueKey = pageRouteValueKey;
            return this;
        }

        /// <summary>
        /// Set the text for previous page navigation.
        /// </summary>
        /// <param name="previousPageText">The previous page text.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetPreviousPageText(string previousPageText)
        {
            base.SetPreviousPageText(previousPageText);
            return this;
        }

        /// <summary>
        /// Set the title for previous page navigation.
        /// </summary>
        /// <param name="previousPageTitle">The previous page title.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetPreviousPageTitle(string previousPageTitle)
        {
            base.SetPreviousPageTitle(previousPageTitle);
            return this;
        }

        /// <summary>
        /// Set the text for next page navigation.
        /// </summary>
        /// <param name="nextPageText">The next page text.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetNextPageText(string nextPageText)
        {
            base.SetNextPageText(nextPageText);
            return this;
        }

        /// <summary>
        /// Set the title for next page navigation.
        /// </summary>
        /// <param name="nextPageTitle">The next page title.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetNextPageTitle(string nextPageTitle)
        {
            base.SetNextPageTitle(nextPageTitle);
            return this;
        }

        /// <summary>
        /// Set the text for first page navigation.
        /// </summary>
        /// <param name="firstPageText">The first page text.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetFirstPageText(string firstPageText)
        {
            base.SetFirstPageText(firstPageText);
            return this;
        }

        /// <summary>
        /// Set the title for first page navigation.
        /// </summary>
        /// <param name="firstPageTitle">The first page title.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetFirstPageTitle(string firstPageTitle)
        {
            base.SetFirstPageTitle(firstPageTitle);
            return this;
        }

        /// <summary>
        /// Set the text for last page navigation.
        /// </summary>
        /// <param name="lastPageText">The last page text.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetLastPageText(string lastPageText)
        {
            base.SetLastPageText(lastPageText);
            return this;
        }

        /// <summary>
        /// Set the title for last page navigation.
        /// </summary>
        /// <param name="lastPageTitle">The last page title.</param>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> SetLastPageTitle(string lastPageTitle)
        {
            base.SetLastPageTitle(lastPageTitle);
            return this;
        }

        /// <summary>
        /// Displays first and last navigation pages.
        /// </summary>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> DisplayFirstAndLastPage()
        {
            base.DisplayFirstAndLastPage();
            return this;
        }

        /// <summary>
        /// Indicate that the total item count means total page count. This option is for scenario's
        /// where certain backends don't return the number of total items, but the number of pages.
        /// </summary>
        /// <returns>PagerOptionsBuilder&lt;TModel&gt;.</returns>
        public new PagerOptionsBuilder<TModel> UseItemCountAsPageCount()
        {
            base.UseItemCountAsPageCount();
            return this;
        }
    }
}