using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestPortal.BL;
using TestPortal.Controllers;

namespace TestPortal.Tests.Controllers
{
    [TestClass]
    public class GroupsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var groupServiceMock = new Mock<IGroupService>();
            var controller = new GroupsController(groupServiceMock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
