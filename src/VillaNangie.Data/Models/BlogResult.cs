using System.Collections.Generic;

namespace VillaNangie.Data.Models
{
    public class BlogResult
    {
        public IEnumerable<BlogStory> Stories;
        public int TotalResults;
        public int TotalPages;
        public int CurrentPage;
    }
}
