using Authentication.Api.Controllers;
using Authentication.Api.Models.Partners;
using Authentication.Api.Services;
using Authentication.Context;
using Authentication.Entity;
using Authentication.Repository;
using Context.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Tests
{
    public class UnitTest1 : IDisposable
    {
        private readonly ContextFactory<AuthenticationContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;

        // setup
        public UnitTest1()
        {
            _contextFactory = new ContextFactory<AuthenticationContext>(true);
            _contextFactory.EnsureCreated();
            var context = _contextFactory.CreateContext();
            _unitOfWork = new UnitOfWork(_contextFactory, new PartnerRepository(context));
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
            var result = await controller.PostPartner(new PartnerRequest() { Name = "Hello" });
            // Assert
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(StatusCodes.Status201Created, viewResult.StatusCode);

            // Act
            var result2 = controller.GetPartner(1);
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
            }
            else
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
            var result2 = await controller2.PostPartner(new PartnerRequest() { Name = "Hello" });
            // Assert
            var viewResult2 = Assert.IsType<CreatedAtActionResult>(result2);

            Assert.Equal(StatusCodes.Status201Created, viewResult2.StatusCode);
            // Act
            var result3 = controller2.GetPartner(1);
            // Assert
            var viewResult3 = Assert.IsType<OkObjectResult>(result3);
            Assert.Equal(StatusCodes.Status200OK, viewResult3.StatusCode);
        }
    }
}