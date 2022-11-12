using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Authentication.Entity;
using Authentication.Api.Models.Partners;
using Authentication.Repository;
using Repository.Common;

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

        /// <summary>
        /// Get list of partners //Should limit number of partners
        /// </summary>
        /// <returns>a list of partners</returns>
        /// <tags>
        /// <tag>Response</tag>
        /// </tags>
        // GET: api/Partners
        [HttpGet]
        //[SwaggerOperation(
        //    Summary = "Creates a new product",
        //    Description = "Requires admin privileges",
        //    OperationId = "CreateProduct",
        //    Tags = new[] { "Purchase", "Products" }
        //)]
        [ProducesResponseType(typeof(IEnumerable<Partner>), StatusCodes.Status200OK)]
        public IActionResult GetPartners()
        {
            return Ok(_partnerRepository.FindAll());
        }

        /// <summary>
        /// Get a partner by identity
        /// </summary>
        /// <param name="id">The primary key of partner</param>
        /// <returns>a partner</returns>
        // GET: api/Partners/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Partner), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        /// <param name="partnerRequest">Data transfer object to update partner</param>
        /// <returns>no content</returns>
        // PUT: api/Partners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutPartner(int id, PartnerRequest partnerRequest)
        {
            var partner = _partnerRepository.FindByID(id);
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
        /// Create new partner with a identify name
        /// </summary>
        /// <param name="partnerRequest">Data transfer object to create partner</param>
        /// <returns>Created partner with new Id</returns>
        // POST: api/Partners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Partner), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostPartner(PartnerRequest partnerRequest)
        {
            var partnerName = partnerRequest.Name.Trim();
            if (_partnerRepository.IsExisted(partnerName))
            {
                return Conflict($"Partner {partnerName} is already in use.");
            }
            var partner = partnerRequest.ToPartner();
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
