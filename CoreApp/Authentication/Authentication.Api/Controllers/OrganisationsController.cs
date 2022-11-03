using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Authentication.Entity;
using Authentication.Repository;
using Repository.Common;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganisationRepository _organisationRepository;

        public OrganisationsController(IUnitOfWork unitOfWork, IOrganisationRepository organisationRepository)
        {
            _unitOfWork = unitOfWork;
            _organisationRepository = organisationRepository;
        }

        // GET: api/Organisations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrganisations()
        {
            return Ok(_organisationRepository.Get());
        }

        // GET: api/Organisations/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetOrganisation(long id)
        {
            var organisation = _organisationRepository.GetByID(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        // PUT: api/Organisations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutOrganisation(long id, Organizer organisation)
        {
            if (id != organisation.Id)
            {
                return BadRequest();
            }
            _organisationRepository.Update(organisation);
            try
            {
                await _unitOfWork.DeadlineAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationExists(id))
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

        // POST: api/Organisations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostOrganisation(Organizer organisation)
        {
            _organisationRepository.Insert(organisation);
            await _unitOfWork.DeadlineAsync();

            return CreatedAtAction("GetOrganisation", new { id = organisation.Id }, organisation);
        }

        // DELETE: api/Organisations/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrganisation(long id)
        {
            var organisation = _organisationRepository.GetByID(id);
            if (organisation == null)
            {
                return NotFound();
            }

            _organisationRepository.Delete(organisation);
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

        private bool OrganisationExists(long id)
        {
            var organisation = _organisationRepository.GetByID(id);
            return organisation != null;
        }
    }
}
