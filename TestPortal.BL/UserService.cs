using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.BL.Base;
using TestPortal.DAL.Entities;
using TestPortal.DAL.Repository;
using TestPortal.DAL.UnitOfWork;

namespace TestPortal.BL
{
    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;        

        public UserService(IUnitOfWork unitOfWork, IRepository<User> userRepository)
            : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
        }


        public User GetById(int id)
        {
            return _unitOfWork.UserRepository.Single(u => u.ID == id);
        }


        public User GetByName(string name)
        {
            return _unitOfWork.UserRepository.Single(u => u.Name == name);
        }

        public bool ValidateUser(string name, string password)
        {
            return _unitOfWork.UserRepository.Find(u => u.Name.ToLower() == name.ToLower() && u.Password == password).Count() > 0;
        }
    }
}
