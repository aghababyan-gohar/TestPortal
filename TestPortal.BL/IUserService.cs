using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.DAL.Entities;
using TestPortal.BL.Base;

namespace TestPortal.BL
{
    public interface IUserService : IEntityService<User>
    {
        User GetById(int Id);

        User GetByName(string name);

        bool ValidateUser(string name, string password);
    }
}
