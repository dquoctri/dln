using Authentication.Api.Controllers;
using Authentication.Api.DTOs;
using Authentication.Context;
using Authentication.Model;
using Authentication.Repository;
using Authentication.Repository.Architectures;
using Context.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Repository.Common;
using Xunit;

namespace Authentication.Tests.Controllers
{
    public class PartnerControllerTest : IDisposable
    {
        private static readonly int NON_EXISTING_ID = 0;

        private readonly IDistributedCache _cache;
        private readonly IContextFactory<AuthenticationContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPartnerRepository _partnerRepository;

        #region SeedData
        private Partner partner1;
        #endregion

        // setup
        public PartnerControllerTest()
        {
            _contextFactory = new SqliteContextFactory<AuthenticationContext>();
            //_contextFactory = new InMemoryContextFactory<AuthenticationContext>();
            _cache = null!;
            var context = _contextFactory.CreateContext();
            _unitOfWork = new UnitOfWork(context);
            _partnerRepository = new PartnerRepository(context);
            partner1 = new Partner() { Name = "Partner Name", Description = "Partner Description" };
            _partnerRepository.Insert(partner1);
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
        public async Task Create_Partner_ReturnsNewPartner()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var result = await controller.PostPartner(new PartnerDTO() { Name = "New Partner", Description = "New Partner Description" });
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal("New Partner", partner.Name);
            Assert.Equal("New Partner Description", partner.Description);
            Assert.Null(partner.ModifiedDate);
        }

        [Fact]
        public async Task Create_Partner_ExistedName_ReturnsConflict()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var result = await controller.PostPartner(new PartnerDTO() { Name = partner1.Name, Description = "Exist Partner Description" });
            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Partner Name is already in use.", errorMsg);
        }

        [Fact]
        public void Get_Partner_ExistedId_ReturnsExistedParter()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
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
        public void Get_Partner_NonExistingId_ReturnsNotFound()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var result = controller.GetPartner(NON_EXISTING_ID);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_Partners_ReturnsAllPartners()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
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
        public async Task Update_Partner_ReturnsNoContent()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var partner = new Partner() { Name = "Second Partner", Description = "Second Partner Description" };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();

            var result = await controller.PutPartner(partner.Id, new PartnerDTO() { Name = "Updated Partner", Description = "Updated Description" });
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            var updatedPartner = _partnerRepository.FindByID(partner.Id);
            Assert.Equal(partner.Id, updatedPartner?.Id);
            Assert.Equal("Updated Partner", updatedPartner?.Name);
            Assert.Equal("Updated Description", updatedPartner?.Description);
            Assert.NotNull(updatedPartner?.ModifiedDate);
        }

        [Fact]
        public async Task Update_Partner_ExistedName_ReturnsConflict()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var partner = new Partner() { Name = "Second Partner", Description = "Second Partner Description" };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();
            var result = await controller.PutPartner(partner.Id, new PartnerDTO() { Name = partner1.Name, Description = "Existed Partner Description" });
            
            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Partner Name is already in use.", errorMsg);
        }

        [Fact]
        public async Task Update_Partner_NonExistingId_ReturnsNotFound()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var result = await controller.PutPartner(NON_EXISTING_ID, new PartnerDTO() { Name = "NotFound Partner", Description = "NotFound Partner Description" });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Partner_ReturnsNoContent()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var partner = new Partner() { Name = "Second Partner", Description = "Second Partner Description" };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();
            var result = await controller.DeletePartner(partner.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedPartner = _partnerRepository.FindByID(partner.Id);
            Assert.Null(deletedPartner);
        }

        [Fact]
        public async Task Delete_Partner_NonExistingId_ReturnsNotFound()
        {
            // Arrange and action
            var controller = new PartnersController(_cache, _unitOfWork, _partnerRepository);
            var result = await controller.DeletePartner(NON_EXISTING_ID);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
