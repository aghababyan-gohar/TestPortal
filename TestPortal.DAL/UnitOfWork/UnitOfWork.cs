using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.DAL.Db;
using TestPortal.DAL.Entities;
using TestPortal.DAL.Repository;

namespace TestPortal.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PortalContext context;
        public PortalContext Context
        {
            get
            {
                if (this.context == null)
                {
                    this.context = new PortalContext();
                }

                return context;
            }
        }

        private IRepository<User> userRepository;
        private IRepository<Group> groupRepository;
        private IRepository<Story> storyRepository;

        public IRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(Context);
                }
                return userRepository;
            }
        }

        public IRepository<Group> GroupRepository
        {
            get
            {
                if (this.groupRepository == null)
                {
                    this.groupRepository = new Repository<Group>(Context);
                }
                return groupRepository;
            }
        }

        public IRepository<Story> StoryRepository
        {
            get
            {
                if (this.storyRepository == null)
                {
                    this.storyRepository = new Repository<Story>(Context);
                }
                return storyRepository;
            }
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
