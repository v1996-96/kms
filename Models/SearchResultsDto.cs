using System.Collections.Generic;

namespace kms.Models
{
    public class SearchResultsDto
    {
        public int Count { get; set; }
        public IEnumerable<SearchResults> Results { get; set; }
    }
}
