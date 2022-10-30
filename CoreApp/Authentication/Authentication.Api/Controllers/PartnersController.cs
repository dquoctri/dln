using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authentication.Entity;
using Authentication.Api.Services;
using Authentication.Api.Models.Partners;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartnersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Partners
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPartners()
        {
            return Ok(_unitOfWork.Partners.Get());
        }

        // GET: api/Partners/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetPartner(int id)
        {
            var partner = _unitOfWork.Partners.GetByID(id);

            if (partner == null)
            {
                return NotFound();
            }

            return Ok(partner);
        }

        // PUT: api/Partners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutPartner(int id, Partner partner)
        {
            if (id != partner.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Partners.Update(partner);

            try
            {
                await _unitOfWork.DeadlineAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerExists(id))
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
        /// <summary>
        /// Create partner by name
        /// </summary>
        /// <param name="partner"></param>
        /// <returns>created partner</returns>
        // POST: api/Partners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostPartner(PartnerRequest partner)
        {
            var existedPartner = _unitOfWork.Partners.GetByName(partner.Name);
            if (existedPartner != null)
            {
                return BadRequest($"{partner.Name} already existed.");
            }
            var _partner = new Partner() { Name = partner.Name, Description = partner.Description };
            _unitOfWork.Partners.Insert(_partner);
            await _unitOfWork.DeadlineAsync();
            return CreatedAtAction("PostPartner", new { id = _partner.Id }, _partner);
        }

        // DELETE: api/Partners/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = _unitOfWork.Partners.GetByID(id);
            if (partner == null)
            {
                return NotFound();
            }

            _unitOfWork.Partners.Delete(partner);
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

        private bool PartnerExists(int id)
        {
            var partner = _unitOfWork.Partners.GetByID(id);
            return partner != null;
        }
    }
}
