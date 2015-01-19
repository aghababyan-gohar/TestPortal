using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestPortal.Models
{
    public class StoryViewModel
    {
        public int StoryID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostOn { get; set; }

        public int UserID { get; set; }

        [Required]
        public int GroupID { get; set; }

        [Display(Name="Groop")]
        public string GroupName { get; set; }
    }
}