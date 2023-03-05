using Authentication.Api.Controllers;
using Authentication.Api.DTOs;
using Authentication.Context;
using Authentication.Model;
using Authentication.Repository;
using Authentication.Repository.Architectures;
using Context.Common;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;

namespace Authentication.Tests.Controllers
{
    public class PartnersControllerTest : IDisposable
    {
        private static readonly int NON_EXISTING_ID = 0;

        private readonly IContextFactory<AuthenticationContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPartnerRepository _partnerRepository;
        private readonly PartnersController _controller;

        #region SeedData
        private Partner partner1;
        #endregion

        // setup
        public PartnersControllerTest()
        {
            _contextFactory = new SqliteContextFactory<AuthenticationContext>();
            //_contextFactory = new InMemoryContextFactory<AuthenticationContext>();
            var context = _contextFactory.CreateContext();
            _unitOfWork = new UnitOfWork(context);
            _partnerRepository = new PartnerRepository(context);
            _controller = new PartnersController(_unitOfWork, _partnerRepository);
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
        public async Task Create_Partner_ReturnsNewPartnerAsync()
        {
            // Arrange and action
            PartnerDTO newPartner = new PartnerDTO()
            {
                Name = "New Partner",
                Description = "New Partner Description"
            };

            var result = await _controller.PostPartner(newPartner);
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal(newPartner.Name, partner.Name);
            Assert.Equal(partner.Description, partner.Description);
            Assert.Null(partner.UpdatedAt);
        }

        [Fact]
        public async Task Create_Partner_ExistedName_ReturnsConflictAsync()
        {
            // Arrange and action
            PartnerDTO partnerDTO = new PartnerDTO()
            {
                Name = partner1.Name,
                Description = "Exist Partner Description"
            };
            var result = await _controller.PostPartner(partnerDTO);
            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Partner Name is already in use.", errorMsg);
        }

        [Fact]
        public void Get_Partner_ExistedId_ReturnsExistedPartner()
        {
            // Arrange and action
            var result = _controller.GetPartner(partner1.Id);
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal(partner1.Id, partner.Id);
            Assert.Equal(partner1.Name, partner.Name);
            Assert.Equal(partner1.Description, partner.Description);
        }

        [Fact]
        public void Get_Partner_NonExistingId_ReturnsNotFound()
        {
            // Arrange and action
            var result = _controller.GetPartner(NON_EXISTING_ID);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_Partners_ReturnsAllPartners()
        {
            // Arrange and action
            Partner secondPartner = new Partner()
            {
                Name = "Second Partner",
                Description = "Second Partner Description"
            };
            _partnerRepository.Insert(secondPartner);
            _unitOfWork.Deadline();
            var result = _controller.GetPartners();
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var partners = Assert.IsAssignableFrom<IEnumerable<Partner>>(viewResult.Value);
            Assert.NotNull(partners);
            Assert.Equal(2, partners.Count());
            var partner = partners.Last();
            Assert.Equal(secondPartner.Name, partner.Name);
            Assert.Equal(secondPartner.Description, partner.Description);
        }

        [Fact]
        public async Task Update_Partner_ReturnsNoContentAsync()
        {
            // Arrange and action
            var secondPartner = new Partner()
            {
                Name = "Second Partner",
                Description = "Second Partner Description"
            };
            _partnerRepository.Insert(secondPartner);
            _unitOfWork.Deadline();
            PartnerDTO partnerDTO = new PartnerDTO()
            {
                Name = "Updated Partner",
                Description = "Updated Description"
            };
            var result = await _controller.PutPartner(secondPartner.Id, partnerDTO);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var partner = Assert.IsType<Partner>(viewResult.Value);
            Assert.Equal(secondPartner.Id, partner.Id);
            Assert.Equal(partnerDTO.Name, partner.Name);
            Assert.Equal(partnerDTO.Description, partner.Description);
        }

        [Fact]
        public async Task Update_Partner_ExistedName_ReturnsConflictAsync()
        {
            // Arrange and action
            var partner = new Partner()
            {
                Name = "Second Partner",
                Description = "Second Partner Description"
            };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();
            PartnerDTO partnerDTO = new PartnerDTO()
            {
                Name = partner1.Name,
                Description = "Existed Partner Description"
            };
            var result = await _controller.PutPartner(partner.Id, partnerDTO);

            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Partner Partner Name is already in use.", errorMsg);
        }

        [Fact]
        public async Task Update_Partner_NonExistingId_ReturnsNotFoundAsync()
        {
            // Arrange and action
            PartnerDTO partnerDTO = new PartnerDTO()
            {
                Name = "NotFound Partner",
                Description = "NotFound Partner Description"
            };
            var result = await _controller.PutPartner(NON_EXISTING_ID, partnerDTO);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Partner_ReturnsNoContentAsync()
        {
            // Arrange and action
            var partner = new Partner() {
                Name = "Second Partner",
                Description = "Second Partner Description"
            };
            _partnerRepository.Insert(partner);
            _unitOfWork.Deadline();
            var result = await _controller.DeletePartner(partner.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedPartner = _partnerRepository.GetByID(partner.Id);
            Assert.Null(deletedPartner);
        }

        [Fact]
        public async Task Delete_Partner_NonExistingId_ReturnsNotFoundAsync()
        {
            // Arrange and action
            var result = await _controller.DeletePartner(NON_EXISTING_ID);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
