using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestPortal.DAL.Entities;
using TestPortal.Models;
using TestPortal.Helpers;

namespace TestPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateMappings();
        }

        private void CreateMappings()
        {
            Mapper.CreateMap<Group, GroupViewModel>()
               .ForMember(d => d.GroupID, opt => opt.MapFrom(s => s.ID))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
               .AfterMap((s, d) =>
                   {
                       if (s.Stories != null)
                           d.StoryCount = s.Stories.Count;
                       d.UserCount = s.Stories.Select(st => st.User.ID).Distinct().Count();
                   })
               .IgnoreAllNonExisting();

            Mapper.CreateMap<Story, StoryViewModel>()
               .ForMember(d => d.StoryID, opt => opt.MapFrom(s => s.ID))
               .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
               .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
               .ForMember(d => d.PostOn, opt => opt.MapFrom(s => s.PostOn))
               .AfterMap((s, d) => 
                   {
                       if (s.Group != null )
                       {
                           d.GroupID = s.Group.ID;
                           d.GroupName = s.Group.Name;
                       }
                   })
               .IgnoreAllNonExisting();

            Mapper.CreateMap<StoryViewModel, Story>()
               .ForMember(d => d.ID, opt => opt.MapFrom(s => s.StoryID))
               .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
               .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
               .ForMember(d => d.PostOn, opt => opt.MapFrom(s => s.PostOn))
               .AfterMap((s, d) =>
                   {
                       
                   })
               .IgnoreAllNonExisting();
        }
    }
}
