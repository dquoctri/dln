using Authentication.Api.DTOs;
using Authentication.Model;
using Authentication.Repository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;
using System.Net.Mime;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizersController(IUnitOfWork unitOfWork, IOrganizerRepository organizerRepository, IPartnerRepository partnerRepository)
        {
            _unitOfWork = unitOfWork;
            _organizerRepository = organizerRepository;
            _partnerRepository = partnerRepository;
        }

        /// <summary>
        /// Get list of organizers //Should limit number of organizers
        /// </summary>
        /// <returns>A list of organizers</returns>
        // GET: api/Organizers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Organizer>), StatusCodes.Status200OK)]
        [ResponseCache(VaryByHeader = "GetOrganizers", Duration = 60)]
        public IActionResult GetOrganizers()
        {
            var organizers = _organizerRepository.FindAll();
            return Ok(organizers);
        }

        /// <summary>
        /// Get an organizer by id
        /// </summary>
        /// <param name="id">Organizer key</param>
        /// <returns>An organizer</returns>
        // GET: api/Organizers/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Organizer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetOrganizer(int id)
        {
            var organizer = _organizerRepository.FindByID(id);
            if (organizer == null) return NotFound();
            return Ok(organizer);
        }

        /// <summary>
        /// Update an existing organizer
        /// </summary>
        /// <param name="id">Organizer key</param>
        /// <param name="organizerDTO">Organizer payload</param>
        /// <returns>Updated organizer</returns>
        // PUT: api/Organizers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Organizer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutOrganizer(int id, OrganizerDTO organizerDTO)
        {
            var organizer = _organizerRepository.FindByID(id);
            if (organizer == null) return NotFound();
            var newOrganizer = organizerDTO.ToOrganizer();
            if (organizer.Name != newOrganizer.Name && _organizerRepository.IsExistedName(organizer.PartnerId, newOrganizer.Name))
            {
                return Conflict($"Organizer {newOrganizer.Name} is already in use.");
            }
            organizer.Name = newOrganizer.Name;
            organizer.Description = newOrganizer.Description;
            organizer.ModifiedDate = DateTime.UtcNow;
            _organizerRepository.Update(organizer);
            await _unitOfWork.DeadlineAsync();
            return Ok(organizer);
        }

        /// <summary>
        /// Create new organizer
        /// </summary>
        /// <param name="organizerDTO">Organizer payload</param>
        /// <returns>new organizer</returns>
        // POST: api/Organizers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostOrganizer(OrganizerDTO organizerDTO)
        {
            var organizer = organizerDTO.ToOrganizer();
            var partner = _partnerRepository.FindByID(organizer.PartnerId);
            if (partner == null) return NotFound($"Partner {organizer.PartnerId} is not found.");
            if (_organizerRepository.IsExistedName(organizer.PartnerId, organizer.Name))
            {
                return Conflict($"Organizer {organizer.Name} is already in use.");
            }

            organizer.Partner = partner;
            _organizerRepository.Insert(organizer);
            await _unitOfWork.DeadlineAsync();
            return CreatedAtAction("GetOrganizer", new { id = organizer.Id }, organizer);
        }

        /// <summary>
        /// Delete organizer by id
        /// </summary>
        /// <param name="id">organizer key</param>
        /// <returns>No Content</returns>
        // DELETE: api/Organizers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrganizer(int id)
        {
            var organizer = _organizerRepository.FindByID(id);
            if (organizer == null) return NotFound();
            _organizerRepository.Delete(organizer);
            await _unitOfWork.DeadlineAsync();

            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError() => Problem();
    }
}
