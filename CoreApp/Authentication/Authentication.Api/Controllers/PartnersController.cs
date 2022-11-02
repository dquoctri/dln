using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authentication.Entity;
using Authentication.Api.Services;
using Authentication.Api.Models.Partners;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Authentication.Repository;

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
            _unitOfWork = unitOfWork;
            _partnerRepository = partnerRepository;
        }

        // GET: api/Partners
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPartners()
        {
            return Ok(_partnerRepository.Get());
        }

        // GET: api/Partners/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetPartner(int id)
        {
            var partner = _partnerRepository.GetByID(id);
            if (partner == null) return NotFound();
            return Ok(partner);
        }

        // PUT: api/Partners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutPartner(int id, PartnerRequest partnerRequest)
        {
            var partner = _partnerRepository.GetByID(id);
            if (partner == null) return NotFound();
            if (partner.Name != partnerRequest.Name && _partnerRepository.IsExisted(partnerRequest.Name.Trim()))
            {
                return Conflict($"Partner {partnerRequest.Name.Trim()} is already in use.");
            }
            partner.Name = partnerRequest.Name.Trim();
            partner.Description = partnerRequest.Description?.Trim();
            partner.ModifiedDate = DateTime.UtcNow;
            _partnerRepository.Update(partner);
            await _unitOfWork.DeadlineAsync();
            return NoContent();
        }

        /// <summary>
        /// Create partner by name
        /// </summary>
        /// <param name="partnerRequest"></param>
        /// <returns>created partner</returns>
        // POST: api/Partners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Partner), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostPartner(PartnerRequest partnerRequest)
        {
            var partnerName = partnerRequest.Name.Trim();
            if (_partnerRepository.IsExisted(partnerName))
            {
                return Conflict($"Partner {partnerName} is already in use.");
            }
            var _partner = partnerRequest.ToPartner();
            _partnerRepository.Insert(_partner);
            await _unitOfWork.DeadlineAsync();
            return CreatedAtAction("PostPartner", new { Id = _partner.Id }, _partner);
        }

        // DELETE: api/Partners/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = _partnerRepository.GetByID(id);
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
