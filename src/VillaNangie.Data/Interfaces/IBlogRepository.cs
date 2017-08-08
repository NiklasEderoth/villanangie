﻿using System.Collections.Generic;
using VillaNangie.Data.Models;

namespace VillaNangie.Data.Interfaces
{
    public interface IBlogRepository
    {
        // Story
        BlogResult GetStories(int pageSize = 10, int page = 1);
        BlogResult GetStoriesByTerm(string term, int pageSize, int page);
        BlogResult GetStoriesByTag(string tag, int pageSize, int page);

        BlogStory GetStory(int id);
        BlogStory GetStory(string slug);
        void AddStory(BlogStory story);

        void SaveAll();
        bool DeleteStory(string postid);

        IEnumerable<string> GetCategories();
    }
}
