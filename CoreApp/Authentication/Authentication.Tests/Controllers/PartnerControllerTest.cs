using Authentication.Api.Controllers;
using Authentication.Api.Models.Partners;
using Authentication.Context;
using Authentication.Entity;
using Authentication.Repository;
using Context.Common;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;
using Xunit;

namespace Authentication.Tests.Controllers
{
    public class PartnerControllerTest : IDisposable
    {
        private static readonly int NOT_FOUND_ID = -4;

        private readonly IContextFactory<AuthenticationContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPartnerRepository _partnerRepository;

        // setup
        public PartnerControllerTest()
        {
            _contextFactory = new SqliteContextFactory<AuthenticationContext>();
            _contextFactory = new InMemoryContextFactory<AuthenticationContext>();
            var context = _contextFactory.CreateContext();
            _unitOfWork = new UnitOfWork(context);
            _partnerRepository = new PartnerRepository(context);
            _partnerRepository.Insert(new Partner() { Name = "Partner Name", Description = "Partner Description" });
            _unitOfWork.Deadline();
        }

        // teardown
        public void Dispose()
        {
            // Dispose here
            if (_unitOfWork is IDisposable unitOfWork) unitOfWork.Dispose();
            if (_contextFactory is IDisposable factory) factory.Dispose();
        }

        [Fact]
        public async Task TestCreatePartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var result = await controller.PostPartner(new PartnerRequest() { Name = "New Partner", Description = "New Partner Description" });
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal("New Partner", partner.Name);
            Assert.Equal("New Partner Description", partner.Description);
            Assert.Null(partner.ModifiedDate);
        }

        [Fact]
        public async Task TestCreateExistPartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var result = await controller.PostPartner(new PartnerRequest() { Name = "Partner Name", Description = "Exist Partner Description" });
            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Partner Name is already in use.", errorMsg);
        }

        [Fact]
        public void TestGetPartnerById()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var result = controller.GetPartner(1);
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal(1, partner.Id);
            Assert.Equal("Partner Name", partner.Name);
            Assert.Equal("Partner Description", partner.Description);
        }

        [Fact]
        public void TestGetNotFoundPartnerById()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var result = controller.GetPartner(NOT_FOUND_ID);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void TestGetAllPartners()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            _partnerRepository.Insert(new Partner() { Name = "Second Partner", Description = "Second Partner Description" });
            _unitOfWork.Deadline();
            var result = controller.GetPartners();
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var partners = Assert.IsAssignableFrom<IEnumerable<Partner>>(viewResult.Value);
            Assert.NotNull(partners);
            Assert.Equal(2, partners.Count());
            var partner = partners.Last();
            Assert.Equal("Second Partner", partner.Name);
            Assert.Equal("Second Partner Description", partner.Description);
        }

        [Fact]
        public async Task TestUpdatePartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var partner = new Partner() { Name = "Second Partner", Description = "Second Partner Description" };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();

            var result = await controller.PutPartner(partner.Id, new PartnerRequest() { Name = "Updated Partner", Description = "Updated Description" });
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            var updatedPartner = _partnerRepository.GetByID(partner.Id);
            Assert.Equal(partner.Id, updatedPartner?.Id);
            Assert.Equal("Updated Partner", updatedPartner?.Name);
            Assert.Equal("Updated Description", updatedPartner?.Description);
            Assert.NotNull(updatedPartner?.ModifiedDate);
        }

        [Fact]
        public async Task TestUpdateConflictPartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var partner = new Partner() { Name = "Second Partner", Description = "Second Partner Description" };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();
            var result = await controller.PutPartner(partner.Id, new PartnerRequest() { Name = "Partner Name", Description = "Existed Partner Description" });
            
            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Partner Name is already in use.", errorMsg);
        }

        [Fact]
        public async Task TestUpdateNotFoundPartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var result = await controller.PutPartner(NOT_FOUND_ID, new PartnerRequest() { Name = "NotFound Partner", Description = "NotFound Partner Description" });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestDeletePartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var partner = new Partner() { Name = "Second Partner", Description = "Second Partner Description" };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();
            var result = await controller.DeletePartner(partner.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedPartner = _partnerRepository.GetByID(partner.Id);
            Assert.Null(deletedPartner);
        }

        [Fact]
        public async Task TestDeleteNotFoundPartner()
        {
            // Arrange and action
            var controller = new PartnersController(_unitOfWork, _partnerRepository);
            var result = await controller.DeletePartner(NOT_FOUND_ID);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
