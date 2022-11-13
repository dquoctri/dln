using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Authentication.Model;
using Repository.Common;
using Authentication.Repository;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizersController(IUnitOfWork unitOfWork, IOrganizerRepository organizerRepository)
        {
            _unitOfWork = unitOfWork;
            _organizerRepository = organizerRepository;
        }

        // GET: api/Organizers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrganizers()
        {
            return Ok(_organizerRepository.FindAll());
        }

        // GET: api/Organizers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetOrganizer(int id)
        {
            var organizer = _organizerRepository.FindByID(id);
            if (organizer == null) return NotFound();
            return Ok(organizer);
        }

        // PUT: api/Organizers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutOrganizer(long id, Organizer organizer)
        {
            if (id != organizer.Id)
            {
                return BadRequest();
            }
            _organizerRepository.Update(organizer);
            try
            {
                await _unitOfWork.DeadlineAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Organizers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostOrganizers(Organizer organizer)
        {
            _organizerRepository.Insert(organizer);
            await _unitOfWork.DeadlineAsync();

            return CreatedAtAction("GetOrganizer", new { id = organizer.Id }, organizer);
        }

        // DELETE: api/Organizers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrganizer(int id)
        {
            var organizer = _organizerRepository.FindByID(id);
            if (organizer == null)
            {
                return NotFound();
            }

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

        private bool OrganizerExists(long id)
        {
            var organizer = _organizerRepository.FindByID(id);
            return organizer != null;
        }
    }
}
