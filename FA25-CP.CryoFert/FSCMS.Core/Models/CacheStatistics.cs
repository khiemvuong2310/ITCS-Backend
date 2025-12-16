using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models
{
    /// <summary>
    /// Used to track 90% cache hit rate target
    /// </summary>
    public class CacheStatistics
    {
        public Dictionary<string, long> Hits { get; set; } = new();
        public Dictionary<string, long> Misses { get; set; } = new();
        public Dictionary<string, long> Sets { get; set; } = new();
        public Dictionary<string, long> Clears { get; set; } = new();
        public Dictionary<string, long> Errors { get; set; } = new();

        /// <summary>
        /// Calculate cache hit rate percentage for a specific key prefix
        /// </summary>
        public double GetHitRate(string keyPrefix)
        {
            var hits = Hits.GetValueOrDefault(keyPrefix, 0);
            var misses = Misses.GetValueOrDefault(keyPrefix, 0);
            var total = hits + misses;

            return total == 0 ? 0 : (double)hits / total * 100;
        }

        /// <summary>
        /// Calculate overall cache hit rate percentage
        /// </summary>
        public double GetOverallHitRate()
        {
            var totalHits = Hits.Values.Sum();
            var totalMisses = Misses.Values.Sum();
            var total = totalHits + totalMisses;

            return total == 0 ? 0 : (double)totalHits / total * 100;
        }

        /// <summary>
        /// Get summary for logging/monitoring
        /// </summary>
        public string GetSummary()
        {
            var hitRate = GetOverallHitRate();
            var totalHits = Hits.Values.Sum();
            var totalMisses = Misses.Values.Sum();
            var totalSets = Sets.Values.Sum();
            var totalClears = Clears.Values.Sum();
            var totalErrors = Errors.Values.Sum();

            return $"Cache Stats: Hit Rate={hitRate:F2}%, Hits={totalHits}, Misses={totalMisses}, Sets={totalSets}, Clears={totalClears}, Errors={totalErrors}";
        }
    }
}
