using AviaMare.Controllers;
using Microsoft.AspNetCore.Mvc;
using UnitTestApp.Controllers;
using Xunit;

namespace AviaMare.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            ControllerForUnitTest controller = new ControllerForUnitTest();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Hello!", result?.ViewData["Message"]);
        }

        [Fact]
        public void IndexViewResultNotNull()
        {
            // Arrange
            ControllerForUnitTest controller = new ControllerForUnitTest();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            ControllerForUnitTest controller = new ControllerForUnitTest();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Index", result?.ViewName);
        }
    }
}