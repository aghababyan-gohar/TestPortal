using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPortal.BL;
using TestPortal.DAL.Entities;
using TestPortal.Models;

namespace TestPortal.Controllers
{
    [Authorize]
    public class StoriesController : Controller
    {
        IStoryService _storyService;
        IGroupService _groupService;
        IUserService _userService;
        private static readonly short PAGE_SIZE = 5;

        public StoriesController(IStoryService storyService, IGroupService groupService, IUserService userService)
        {
            _storyService = storyService;
            _groupService = groupService;
            _userService = userService;
        }
        
        public ActionResult Index(int page = 1)
        {
            var userName = HttpContext.User.Identity.Name;            
            int total;
            
            var dbStories = _storyService.GetPaged(page - 1, PAGE_SIZE, s => s.User.Name.ToLower() == userName, "PostOn", "DESC",out total).ToList();
            var stories = Mapper.Map<List<Story>, List<StoryViewModel>>(dbStories);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = total;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / PAGE_SIZE);

            return View(stories);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var dbStory = _storyService.GetById(id);
            var model = Mapper.Map<Story, StoryViewModel>(dbStory);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GetGroupsInViewbag();

            return View();
        }

        [HttpPost]
        public ActionResult Create(StoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                GetGroupsInViewbag();
                return View();
            }

            try
            {
                model.UserID = _userService.GetByName(HttpContext.User.Identity.Name).ID;
                model.PostOn = DateTime.Now;
                var story = Mapper.Map<StoryViewModel, Story>(model);
                _storyService.AddStory(story);
            }
            catch (Exception ex)
            {
                //todo: add logging here
                ModelState.AddModelError("", "The error is occurred. Please try again.");
                GetGroupsInViewbag();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dbStory = _storyService.GetById(id);
            var model = Mapper.Map<Story, StoryViewModel>(dbStory);

            GetGroupsInViewbag(model.GroupID.ToString());

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                GetGroupsInViewbag();
                return View();
            }

            try
            {
                var story = Mapper.Map<StoryViewModel, Story>(model);
                _storyService.UpdateStory(story);
            }
            catch (Exception ex)
            {
                //todo: add logging here
                ModelState.AddModelError("", "The error is occurred. Please try again.");
                GetGroupsInViewbag();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private void GetGroupsInViewbag(string selectedGroup = "")
        {
            var dbGroups = _groupService.GetAll().ToList();
            ViewBag.Groups = String.IsNullOrEmpty(selectedGroup) ? new SelectList(dbGroups, "ID", "Name") : new SelectList(dbGroups, "ID", "Name", selectedGroup);
        }
    }
}