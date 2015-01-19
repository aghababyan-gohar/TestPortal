using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPortal.DAL.Entities
{
    public class Story
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostOn { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
