using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestPortal.Models
{
    public class GroupViewModel
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCount { get; set; }
        public int StoryCount { get; set; }
    }
}