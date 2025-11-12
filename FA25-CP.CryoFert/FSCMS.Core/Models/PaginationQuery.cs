using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Interfaces;

namespace FSCMS.Core.Models
{
    public abstract class PaginationQuery<T> : IPaginationQuery
    {
        protected abstract IEnumerable<string> ValidSortProperties { get; }
        protected abstract IEnumerable<string> ValidFilterProperties { get; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = "asc";
        public string? Filter { get; set; }
        public string? Search { get; set; }
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
        public virtual bool IsValidSortProperty(string propertyName)
        {
            return !string.IsNullOrWhiteSpace(propertyName) &&
                   ValidSortProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
        }
        public virtual bool IsValidFilterProperty(string propertyName)
        {
            return !string.IsNullOrWhiteSpace(propertyName) &&
                   ValidFilterProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
        }
        public abstract Expression<Func<T, bool>>? GetFilterExpression();
        public abstract (Expression<Func<T, object>> SortExpression, bool IsAscending) GetSortExpression();
    }
}
