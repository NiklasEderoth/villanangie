﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilderMinds.MetaWeblog;

namespace VillaNangie.Web.WebLogProvider
{
    public class OpenWebLogProvider : IMetaWeblogProvider
    {
        public int AddCategory(string key, string username, string password, NewCategory category)
        {
            throw new NotImplementedException();
        }

        public string AddPost(string blogid, string username, string password, Post post, bool publish)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(string key, string postid, string username, string password, bool publish)
        {
            throw new NotImplementedException();
        }

        public bool EditPost(string postid, string username, string password, Post post, bool publish)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo[] GetCategories(string blogid, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(string postid, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserInfo(string key, string username, string password)
        {
            throw new NotImplementedException();
        }

        public BlogInfo[] GetUsersBlogs(string key, string username, string password)
        {
            throw new NotImplementedException();
        }

        public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
        {
            throw new NotImplementedException();
        }
    }
}
