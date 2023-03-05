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
    public class OrganizersControllerTest : IDisposable
    {
        private static readonly int NON_EXISTING_ID = 0;

        private readonly IContextFactory<AuthenticationContext> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly OrganizersController _controller;

        #region Define SeedData
        private Partner partner1;
        private Organizer organizer1;
        #endregion

        // setup
        public OrganizersControllerTest()
        {
            _contextFactory = new SqliteContextFactory<AuthenticationContext>();
            //_contextFactory = new InMemoryContextFactory<AuthenticationContext>();
            var context = _contextFactory.CreateContext();
            _unitOfWork = new UnitOfWork(context);
            _organizerRepository = new OrganizerRepository(context);
            _partnerRepository = new PartnerRepository(context);
            _controller = new OrganizersController(_unitOfWork, _organizerRepository, _partnerRepository);

            //Init SeedData
            partner1 = new Partner() { Name = "Partner Name", Description = "Partner Description" };
            organizer1 = new Organizer() { Name = "Organizer Name", Description = "Organizer Description", Partner = partner1 };
            _organizerRepository.Insert(organizer1);
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
        public async Task Create_Organizer_ReturnsNewOrganizerAsync()
        {
            // Arrange and action
            OrganizerDTO newOrganizer = new OrganizerDTO()
            {
                Name = "New Organizer",
                Description = "New Organizer Description",
                PartnerId = partner1.Id
            };
            var result = await _controller.PostOrganizer(newOrganizer);
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);
            var organizer = Assert.IsType<Organizer>(viewResult.Value);
            Assert.Equal(newOrganizer.Name, organizer.Name);
            Assert.Equal(newOrganizer.Description, organizer.Description);
            Assert.Null(organizer.UpdateAt);
        }

        [Fact]
        public async Task Create_Organizer_ExistedName_ReturnsConflictAsync()
        {
            // Arrange and action
            OrganizerDTO organizer = new OrganizerDTO()
            {
                Name = organizer1.Name,
                Description = "Existed Organizer Description",
                PartnerId = partner1.Id
            };
            var result = await _controller.PostOrganizer(organizer);
            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal($"Organizer {organizer1.Name} is already in use.", errorMsg);
        }

        [Fact]
        public async Task Create_Organizer_ExistedName_DiffPartner_NewOrganizerAsync()
        {
            // Arrange and action
            Partner secondPartner = new Partner()
            {
                Name = "Second Partner",
                Description = "Second Partner Description"
            };
            _partnerRepository.Insert(secondPartner);
            _unitOfWork.Deadline();

            OrganizerDTO organizerDTO = new OrganizerDTO()
            {
                Name = organizer1.Name,
                Description = "Existed Organizer Description",
                PartnerId = secondPartner.Id
            };
            var result = await _controller.PostOrganizer(organizerDTO);
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<CreatedAtActionResult>(result);
            var organizer = Assert.IsType<Organizer>(viewResult.Value);
            Assert.Equal(organizer1.Name, organizer.Name);
            Assert.Equal("Existed Organizer Description", organizer.Description);
            Assert.Null(organizer.UpdateAt);
        }

        [Fact]
        public void Get_Organizer_ExistedId_ReturnsExistedOrganizer()
        {
            // Arrange and action
            var result = _controller.GetOrganizer(organizer1.Id);
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var organizer = Assert.IsType<Organizer>(viewResult.Value);
            Assert.Equal(organizer1.Id, organizer.Id);
            Assert.Equal(organizer1.Name, organizer.Name);
            Assert.Equal(organizer1.Description, organizer.Description);
        }

        [Fact]
        public void Get_Organizer_NonExistingId_ReturnsNotFound()
        {
            // Arrange and action
            var result = _controller.GetOrganizer(NON_EXISTING_ID);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_Organizers_ReturnsAllOrganizers()
        {
            // Arrange and action
            Organizer secondOrganizer = new Organizer()
            {
                Name = "Second Organizer",
                Description = "Second Organizer Description",
                PartnerId = partner1.Id,
                Partner = partner1,
            };
            _organizerRepository.Insert(secondOrganizer);
            _unitOfWork.Deadline();
            var result = _controller.GetOrganizers();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var organizers = Assert.IsAssignableFrom<IEnumerable<Organizer>>(viewResult.Value);
            Assert.NotNull(organizers);
            Assert.Equal(2, organizers.Count());
            var organizer = organizers.Last();
            Assert.Equal(secondOrganizer.Name, organizer.Name);
            Assert.Equal(secondOrganizer.Description, organizer.Description);
        }

        [Fact]
        public async Task Update_Organizer_ReturnsNoContentAsync()
        {
            // Arrange and action
            Organizer secondOrganizer = new Organizer()
            {
                Name = "Second Organizer",
                Description = "Second Organizer Description",
                PartnerId = partner1.Id,
                Partner = partner1,
            };
            _organizerRepository.Insert(secondOrganizer);
            _unitOfWork.Deadline();

            OrganizerDTO organizerDTO = new OrganizerDTO()
            {
                Name = "Updated Organizer",
                Description = "Updated Description",
                PartnerId = partner1.Id
            };
            var result = await _controller.PutOrganizer(secondOrganizer.Id, organizerDTO);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var organizer = Assert.IsType<Organizer>(viewResult.Value);
            Assert.Equal(secondOrganizer.Id, organizer.Id);
            Assert.Equal(organizerDTO.Name, organizer.Name);
            Assert.Equal(organizerDTO.Description, organizer.Description);
        }

        [Fact]
        public async Task Update_Organizer_ExistedName_ReturnsConflictAsync()
        {
            // Arrange and action
            Organizer secondOrganizer = new Organizer()
            {
                Name = "Second Organizer",
                Description = "Second Organizer Description",
                PartnerId = partner1.Id,
                Partner = partner1,
            };
            _organizerRepository.Insert(secondOrganizer);
            _unitOfWork.Deadline();

            OrganizerDTO organizerDTO = new OrganizerDTO()
            {
                Name = organizer1.Name,
                Description = "Existed Organizer Description",
                PartnerId = partner1.Id,
            };

            var result = await _controller.PutOrganizer(secondOrganizer.Id, organizerDTO);

            // Assert
            var viewResult = Assert.IsType<ConflictObjectResult>(result);
            var errorMsg = Assert.IsType<string>(viewResult.Value);
            Assert.Equal("Organizer Organizer Name is already in use.", errorMsg);
        }

        [Fact]
        public async Task Update_Organizer_NonExistingId_ReturnsNotFoundAsync()
        {
            // Arrange and action
            OrganizerDTO organizerDTO = new OrganizerDTO()
            {
                Name = "NotFound Organizer",
                Description = "NotFound Organizer Description",
                PartnerId = partner1.Id,
            };
            var result = await _controller.PutOrganizer(NON_EXISTING_ID, organizerDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Organizer_ReturnsNoContentAsync()
        {
            // Arrange and action
            Organizer secondOrganizer = new Organizer()
            {
                Name = "Second Organizer",
                Description = "Second Organizer Description",
                PartnerId = partner1.Id,
                Partner = partner1,
            };
            _organizerRepository.Insert(secondOrganizer);
            _unitOfWork.Deadline();
            var result = await _controller.DeleteOrganizer(secondOrganizer.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedOrganizer = _organizerRepository.GetByID(secondOrganizer.Id);
            Assert.Null(deletedOrganizer);
        }

        [Fact]
        public async Task Delete_Organizer_NonExistingId_ReturnsNotFoundAsync()
        {
            // Arrange and action
            var result = await _controller.DeleteOrganizer(NON_EXISTING_ID);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
