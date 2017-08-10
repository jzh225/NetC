using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Class PagingExtensions.
    /// </summary>
    public static class PagingExtensions
    {
        #region HtmlHelper extensions

        /// <summary>
        /// Pagers the specified page size.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <returns>Pager.</returns>
        public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
        {
            return new Pager(htmlHelper, pageSize, currentPage, totalItemCount);
        }

        /// <summary>
        /// Pagers the specified page size.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <returns>Pager.</returns>
        public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
        {
            return new Pager(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
        }

        /// <summary>
        /// Pagers the specified page size.
        /// </summary>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <returns>Pager&lt;TModel&gt;.</returns>
        public static Pager<TModel> Pager<TModel>(this HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount)
        {
            return new Pager<TModel>(htmlHelper, pageSize, currentPage, totalItemCount);
        }

        /// <summary>
        /// Pagers the specified page size.
        /// </summary>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <returns>Pager&lt;TModel&gt;.</returns>
        public static Pager<TModel> Pager<TModel>(this HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
        {
            return new Pager<TModel>(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
        }

        #endregion

        #region IQueryable<T> extensions

        /// <summary>
        /// To the paged list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>IPagedList&lt;T&gt;.</returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int? totalCount = null)
        {
            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
        }

        #endregion

        #region IEnumerable<T> extensions

        /// <summary>
        /// To the paged list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>IPagedList&lt;T&gt;.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int? totalCount = null)
        {
            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
        }

        #endregion
    }
}