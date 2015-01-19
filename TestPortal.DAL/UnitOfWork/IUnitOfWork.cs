using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.DAL.Entities;
using TestPortal.DAL.Repository;

namespace TestPortal.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Group> GroupRepository { get; }
        IRepository<Story> StoryRepository { get; }
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        void Save();
    }
}
