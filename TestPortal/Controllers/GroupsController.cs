using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPortal.BL;
using TestPortal.DAL.Entities;
using TestPortal.Models;
using PagedList;

namespace TestPortal.Controllers
{
    public class GroupsController : Controller
    {
        IGroupService _groupService;
        private static readonly short PAGE_SIZE = 10;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        // GET: Groups
        public ActionResult Index(int page = 1)
        {
            int total;
            var dbGroups = _groupService.GetPaged(page - 1, PAGE_SIZE, s => s.Name.Contains(""), "Name", "ASC", out total).ToList();
            var groups = Mapper.Map<List<Group>, List<GroupViewModel>>(dbGroups);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = total;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / PAGE_SIZE);

            return View(groups);
        }
    }
}