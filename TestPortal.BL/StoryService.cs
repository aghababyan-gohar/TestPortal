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
    public class StoryService: EntityService<Story>, IStoryService
    {
        IUnitOfWork _unitOfWork;
        //IRepository<Story> _storyRepository;

        public StoryService(IUnitOfWork unitOfWork, IRepository<Story> storyRepository)
            : base(unitOfWork, storyRepository)
        {
            _unitOfWork = unitOfWork;
            //_storyRepository = storyRepository;
        }


        public Story GetById(int id)
        {
            return _unitOfWork.StoryRepository.Single(u => u.ID == id);
        }


        public void AddStory(Story story)
        {
            _unitOfWork.StoryRepository.Add(story);
            _unitOfWork.Save();
        }

        public void UpdateStory(Story story)
        {
            _unitOfWork.StoryRepository.Edit(story);
            _unitOfWork.Save();
        }
    }
}