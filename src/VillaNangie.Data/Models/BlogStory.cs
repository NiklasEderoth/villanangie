using System;

namespace VillaNangie.Data.Models
{
    public class BlogStory
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Categories { get; set; }
        public DateTimeOffset DatePublished { get; set; }
        public bool IsPublished { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string UniqueId { get; set; }
    }
}