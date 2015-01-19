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
    public class GroupService : EntityService<Group>, IGroupService
    {
        IUnitOfWork _unitOfWork;
        IRepository<Group> _groupRepository;

        public GroupService(IUnitOfWork unitOfWork, IRepository<Group> groupRepository)
            : base(unitOfWork, groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }


        public Group GetById(int id)
        {
            return _groupRepository.Single(u => u.ID == id);
        }
    }
}
