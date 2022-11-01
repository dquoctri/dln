using Authentication.Api.Controllers;
using Authentication.Api.Models.Partners;
using Authentication.Api.Services;
using Authentication.Context;
using Authentication.Entity;
using Authentication.Repository;
using Context.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Tests.Controllers
{
    public class PartnerControllerTest : IDisposable
    {
        private readonly IContextFactory<AuthenticationContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;

        // setup
        public PartnerControllerTest()
        {
            _contextFactory = new SqliteContextFactory<AuthenticationContext>();
            AuthenticationContext context = _contextFactory.CreateContext();
            _unitOfWork = new UnitOfWork(_contextFactory, new PartnerRepository(context));
        }

        // teardown
        public void Dispose()
        {
            // Dispose here
            if (_contextFactory is IDisposable factory)
            {
                factory.Dispose();
            }
        }

        [Fact]
        public async Task TestCreatePartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork);
            var result = await controller.PostPartner(new PartnerRequest() { Name = "New Partner", Description = "New Partner Description" });
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, viewResult.StatusCode);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal("New Partner", partner.Name);
            Assert.Equal("New Partner Description", partner.Description);
        }

        [Fact]
        public async Task TestExistPartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork);
            var result = await controller.PostPartner(new PartnerRequest() { Name = "Exist Partner", Description = "Exist Partner Description" });
            // Assert
            Assert.NotNull(result);
            var controller2 = new PartnersController(_unitOfWork);
            var result2 = await controller2.PostPartner(new PartnerRequest() { Name = "Exist Partner", Description = "Exist Partner Description" });
            var viewResult = Assert.IsType<ConflictObjectResult>(result2);
            Assert.Equal(StatusCodes.Status409Conflict, viewResult.StatusCode);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Exist Partner is already in use.", errorMsg);
        }


        [Fact]
        public async Task Test3Async()
        {
            // Arrange
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
    }
}
