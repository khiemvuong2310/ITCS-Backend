using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models
{
    /// <summary>
    /// Abstract base class for pagination queries with sorting and filtering capabilities.
    /// </summary>
    /// <typeparam name="T">The type of entity being queried.</typeparam>
    public abstract class PaginationQuery<T> : IPaginationQuery
    {
        /// <summary>
        /// Gets or sets the list of valid properties for sorting.
        /// </summary>
        protected abstract IEnumerable<string> ValidSortProperties { get; }

        /// <summary>
        /// Gets or sets the list of valid properties for filtering.
        /// </summary>
        protected abstract IEnumerable<string> ValidFilterProperties { get; }

        //// <inheritdoc/>
        public int Page { get; set; } = 1;

        //// <inheritdoc/>
        public int Size { get; set; } = 10;

        //// <inheritdoc/>
        public string SortBy { get; set; } = string.Empty;

        //// <inheritdoc/>
        public string SortDirection { get; set; } = "asc";

        //// <inheritdoc/>
        public string? Filter { get; set; }

        /// <inheritdoc/>
        public string? Search { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string> ParseFilterDictionary()
        {
            var filterDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(Filter))
                return filterDict;

            var filterParts = Filter.Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in filterParts)
            {
                var keyValue = part.Split('=', 2);
                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].Trim();
                    var value = keyValue[1].Trim();

                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        filterDict[key] = value;
                    }
                }
            }

            return filterDict;
        }

        /// <inheritdoc/>
        public virtual bool IsValidSortProperty(string propertyName)
        {
            return !string.IsNullOrWhiteSpace(propertyName) &&
                   ValidSortProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public virtual bool IsValidFilterProperty(string propertyName)
        {
            return !string.IsNullOrWhiteSpace(propertyName) &&
                   ValidFilterProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Generates a filter expression based on the provided filter dictionary.
        /// </summary>
        /// <returns>
        /// An expression that evaluates to true for entities matching the filter criteria.
        /// </returns>
        public abstract Expression<Func<T, bool>>? GetFilterExpression();

        /// <summary>
        /// Generates a sort expression based on the specified sorting criteria.
        /// </summary>
        /// <returns>
        /// A tuple containing the sort expression and a boolean indicating the sort direction.
        /// </returns>
        public abstract (Expression<Func<T, object>> SortExpression, bool IsAscending) GetSortExpression();
    }
}
