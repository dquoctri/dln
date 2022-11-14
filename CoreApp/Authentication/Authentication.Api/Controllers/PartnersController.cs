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
    public class PartnersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPartnerRepository _partnerRepository;

        public PartnersController(IUnitOfWork unitOfWork, IPartnerRepository partnerRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _partnerRepository = partnerRepository ?? throw new ArgumentNullException(nameof(partnerRepository));
        }

        /// <summary>
        /// Get list of partners //Should limit number of partners
        /// </summary>
        /// <returns>a list of partners</returns>
        // GET: api/Partners
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Partner>), StatusCodes.Status200OK)]
        [ResponseCache(VaryByHeader = "GetPartners", Duration = 60)]
        public IActionResult GetPartners()
        {
            var partners = _partnerRepository.FindAll();
            return Ok(partners);
        }

        /// <summary>
        /// Get a partner by id
        /// </summary>
        /// <param name="id">The primary key of partner</param>
        /// <returns>A partner</returns>
        // GET: api/Partners/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Partner), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ResponseCache(VaryByHeader = "GetPartner", Duration = 60)]
        public IActionResult GetPartner(int id)
        {
            var partner = _partnerRepository.FindByID(id);
            if (partner == null) return NotFound();
            return Ok(partner);
        }

        /// <summary>
        /// Update an existing partner
        /// </summary>
        /// <param name="id">The primary key of partner</param>
        /// <param name="partnerDto">Data transfer object to update partner</param>
        /// <returns>no content</returns>
        // PUT: api/Partners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Partner), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutPartner(int id, PartnerDTO partnerDto)
        {
            var partner = _partnerRepository.FindByID(id);
            if (partner == null) return NotFound();
            var newPartner = partnerDto.ToPartner();
            if (partner.Name != newPartner.Name && _partnerRepository.IsExistedName(newPartner.Name))
            {
                return Conflict($"Partner {newPartner.Name} is already in use.");
            }
            partner.Name = newPartner.Name;
            partner.Description = newPartner.Description;
            partner.ModifiedDate = DateTime.UtcNow;
            _partnerRepository.Update(partner);
            await _unitOfWork.DeadlineAsync();
            return Ok(partner);
        }

        /// <summary>
        /// Create new partner with a identify name
        /// </summary>
        /// <param name="partnerDto">Data transfer object to create partner</param>
        /// <returns>Created partner with new Id</returns>
        // POST: api/Partners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Partner), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostPartner(PartnerDTO partnerDto)
        {
            var partner = partnerDto.ToPartner();
            if (_partnerRepository.IsExistedName(partner.Name))
            {
                return Conflict($"Partner {partner.Name} is already in use.");
            }
            _partnerRepository.Insert(partner);
            await _unitOfWork.DeadlineAsync();
            return CreatedAtAction("PostPartner", new { Id = partner.Id }, partner);
        }

        /// <summary>
        /// Soft delete an existing partner
        /// </summary>
        /// <param name="id">The primary key of partner</param>
        /// <returns>no content</returns>
        // DELETE: api/Partners/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = _partnerRepository.FindByID(id);
            if (partner == null) return NotFound();
            // TODO: should use soft delete instead of hard delete
            _partnerRepository.Delete(partner);
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
