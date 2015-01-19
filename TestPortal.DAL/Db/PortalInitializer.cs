using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.DAL.Entities;

namespace TestPortal.DAL.Db
{
    public class PortalInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PortalContext>
    {
        protected override void Seed(PortalContext context)
        {
            var users = new List<User>
            {
                new User{ Name = "Ann", Password = "123456" },
                new User{ Name = "John", Password = "111111" },
                new User{ Name = "Jane", Password = "222222" }
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var groups = new List<Group>
            {
                new Group 
                { 
                    Name = "Travelling", 
                    Description = "There are dfferent stories about traveling to different countries and places from all over the world"
                },
                new Group 
                { 
                    Name = "Cooking", 
                    Description = "There are stories about kitchen, cooking and food."
                },
                new Group
                { 
                    Name = "Science", 
                    Description = "There are stories and scientific facts from different aspects of science."
                },
                new Group
                { 
                    Name = "Social", 
                    Description = "There are stories about social life of users."
                },
                new Group
                { 
                    Name = "Art", 
                    Description = "There are stories about different spheres of art."
                }
            };
            groups.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();

            var stories = new List<Story>
            {
                new Story 
                { 
                    UserID = 1, 
                    GroupID = 1,
                    Title = "Once upon a time in India", 
                    Description = "About my last travelling to India", 
                    Content = "About my last travelling to India", 
                    PostOn = DateTime.Now.AddDays(-1)
                },
                new Story 
                { 
                    UserID = 1, 
                    GroupID = 2,
                    Title = "A funny story", 
                    Description = "About a story from cooking", 
                    Content = "It's a story about my last troubles with cooking a cake.", 
                    PostOn = DateTime.Now.AddDays(-7).AddMinutes(28)
                }
            };
            stories.ForEach(s => context.Stories.Add(s));
            context.SaveChanges();
        }
    }
}
