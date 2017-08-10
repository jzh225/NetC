using System.Collections.Generic;

namespace Weeho.Common.MvcPaging
{
    /// <summary>
    /// Interface IPagedList
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// Gets the page count.
        /// </summary>
        /// <value>The page count.</value>
        int PageCount { get; }
        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        int TotalItemCount { get; }
        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        int PageIndex { get; }
        /// <summary>
        /// Gets the page number.
        /// </summary>
        /// <value>The page number.</value>
        int PageNumber { get; }
        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        int PageSize { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value><c>true</c> if this instance has previous page; otherwise, <c>false</c>.</value>
        bool HasPreviousPage { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value><c>true</c> if this instance has next page; otherwise, <c>false</c>.</value>
        bool HasNextPage { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is first page.
        /// </summary>
        /// <value><c>true</c> if this instance is first page; otherwise, <c>false</c>.</value>
        bool IsFirstPage { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is last page.
        /// </summary>
        /// <value><c>true</c> if this instance is last page; otherwise, <c>false</c>.</value>
        bool IsLastPage { get; }
        /// <summary>
        /// Gets the item start.
        /// </summary>
        /// <value>The item start.</value>
        int ItemStart { get; }
        /// <summary>
        /// Gets the item end.
        /// </summary>
        /// <value>The item end.</value>
        int ItemEnd { get; }
    }

    /// <summary>
    /// Interface IPagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Common.MvcPaging.IPagedList" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
    }
}