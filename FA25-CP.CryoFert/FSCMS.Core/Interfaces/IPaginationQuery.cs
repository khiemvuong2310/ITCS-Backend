using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Interfaces
{
    public interface IPaginationQuery
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        string? Search { get; set; }

        /// <summary>
        /// Gets or sets the sort direction (ascending or descending).
        /// </summary>
        string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the property to sort by.
        /// </summary>
        string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the filter string.
        /// </summary>
        string? Filter { get; set; }

        /// <summary>
        /// Parses the filter string into a dictionary of property-value pairs.
        /// </summary>
        /// <returns>A dictionary containing property names and their corresponding filter values.</returns>
        Dictionary<string, string> ParseFilterDictionary();

        /// <summary>
        /// Validates if the provided property name is valid for sorting.
        /// </summary>
        /// <param name="propertyName">The property name to validate.</param>
        /// <returns>True if the property is valid for sorting, otherwise false.</returns>
        bool IsValidSortProperty(string propertyName);

        /// <summary>
        /// Validates if the provided property name is valid for filtering.
        /// </summary>
        /// <param name="propertyName">The property name to validate.</param>
        /// <returns>True if the property is valid for filtering, otherwise false.</returns>
        bool IsValidFilterProperty(string propertyName);
    }
}
