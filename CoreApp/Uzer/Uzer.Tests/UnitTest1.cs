using Context.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzer.Api.Controllers;
using Uzer.Api.Services;
using Uzer.Context;
using Uzer.Entity;
using Uzer.Repository;
using Xunit;

namespace Uzer.Tests
{
    public class UnitTest1 : IDisposable
    {
        private readonly ContextFactory<UserContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;

        // setup
        public UnitTest1()
        {
            _contextFactory = new ContextFactory<UserContext>(true);
            UserContext? userContext = _contextFactory.Create();
            if (userContext == null)
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseInMemoryDatabase(databaseName: "dln_uzer").Options;
                userContext = new UserContext(options);
            }
            _contextFactory.EnsureCreated();
            _unitOfWork = new UnitOfWork(userContext);
        }

        // teardown
        public void Dispose()
        {
            _contextFactory.EnsureDeleted();
            // Dispose here
        }


        [Fact]
        public async Task Test3Async()
        {
            // Arrange
            //var mockRepo = new Mock<IUserRepository>();
            //mockRepo.Setup(repo => repo.ListAsync())
            //    .ReturnsAsync(GetTestSessions());
            var controller = new PartnersController(_unitOfWork);

            // Act
            var result = await controller.PostPartner(new Partner() { Name = "Hello" });
            // Assert
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(StatusCodes.Status201Created, viewResult.StatusCode);

            // Act
            var result2 = controller.GetPartner(1L);
            // Assert
            var viewResult2 = Assert.IsType<OkObjectResult>(result2);
            Assert.Equal(StatusCodes.Status200OK, viewResult2.StatusCode);
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            //var mockRepo = new Mock<IUserRepository>();
            //mockRepo.Setup(repo => repo.ListAsync())
            //    .ReturnsAsync(GetTestSessions());
            var controller = new UsersController(_unitOfWork);

            // Act
            var result = controller.GetUsers();
            // Assert
             var viewResult = Assert.IsType<OkObjectResult>(result);
            if (viewResult.Value is IEnumerable<User> users)
            {
                Assert.Empty(users);
            } else
            {
                Assert.False(true);
            } 
        }

        [Fact]
        public async Task Test2Async()
        {
            // Arrange
            //var mockRepo = new Mock<IUserRepository>();
            //mockRepo.Setup(repo => repo.ListAsync())
            //    .ReturnsAsync(GetTestSessions());
            var controller = new UsersController(_unitOfWork);

            // Act
            var result = controller.GetUser(2);
            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, viewResult.StatusCode);



            var controller2 = new PartnersController(_unitOfWork);

            // Act
            var result2 = await controller2.PostPartner(new Partner() { Name = "Hello" });
            // Assert
            var viewResult2 = Assert.IsType<CreatedAtActionResult>(result2);

            Assert.Equal(StatusCodes.Status201Created, viewResult2.StatusCode);
            // Act
            var result3 = controller2.GetPartner(1L);
            // Assert
            var viewResult3 = Assert.IsType<OkObjectResult>(result3);
            Assert.Equal(StatusCodes.Status200OK, viewResult3.StatusCode);
        }
    }
}