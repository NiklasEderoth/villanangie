using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using VillaNangie.Data.Models;

namespace VillaNangie.Data.Utils
{
    public class BlogDBSeeder
    {
        private BlogContext _ctx;
        private UserManager<BlogUser> _userMgr;

        public BlogDBSeeder(BlogContext ctx, UserManager<BlogUser> userMgr)
        {
            _ctx = ctx;
            _userMgr = userMgr;
        }

        public async Task SeedAsync()
        {
            // Seed User
            if (await _userMgr.FindByNameAsync("shawnwildermuth") == null)
            {
                var user = new BlogUser()
                {
                    Email = "niklas.ederoth@gmail.com",
                    UserName = "ederoth",
                    EmailConfirmed = true
                };

                var result = await _userMgr.CreateAsync(user, "P@ssw0rd!"); // Temp Password
                if (!result.Succeeded) throw new InvalidProgramException("Failed to create seed user");
            }
        }
    }
}
