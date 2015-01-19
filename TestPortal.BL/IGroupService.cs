using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.BL.Base;
using TestPortal.DAL.Entities;

namespace TestPortal.BL
{
    public interface IGroupService : IEntityService<Group>
    {
        Group GetById(int Id);
    }

}
