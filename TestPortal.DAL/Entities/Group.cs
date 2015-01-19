using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPortal.DAL.Entities
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Story> Stories { get; set; }
    }
}
