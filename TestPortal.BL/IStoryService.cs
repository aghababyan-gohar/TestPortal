using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.BL.Base;
using TestPortal.DAL.Entities;

namespace TestPortal.BL
{
    public interface IStoryService : IEntityService<Story>
    {
        Story GetById(int Id);

        void AddStory(Story story);

        void UpdateStory(Story story);
    }

}
