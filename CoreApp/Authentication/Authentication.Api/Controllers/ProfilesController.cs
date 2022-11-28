using Microsoft.AspNetCore.Mvc;
using Authentication.Repository;
using Repository.Common;
using Authentication.Api.DTOs;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProfileRepository _profileRepository;

        public ProfilesController(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            _unitOfWork = unitOfWork;
            _profileRepository = profileRepository;
        }

        // GET: api/Profiles
        [HttpGet]
        public IActionResult GetProfiles()
        {
            return Ok(_profileRepository.GetAll());
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            var profile = _profileRepository.GetByID(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, ProfileDTO profileDTO)
        {
            var profile = _profileRepository.GetByID(id);
            if (profile == null) return NotFound();
            var newProfile = profileDTO.ToProfile();
            if (profile.Name != newProfile.Name && _profileRepository.IsExistedName(newProfile.Name))
            {
                return Conflict($"Profile {newProfile.Name} is already in use.");
            }
            profile.Name= newProfile.Name;
            profile.Description = newProfile.Description;
            _profileRepository.Update(profile);
            await _unitOfWork.DeadlineAsync();
            return Ok(profile);
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProfile(ProfileDTO profileDTO)
        {
            var profile = profileDTO.ToProfile();
            if (_profileRepository.IsExistedName(profile.Name))
            {
                return Conflict($"Profile {profile.Name} is already in use.");
            }
            _profileRepository.Insert(profile);
            await _unitOfWork.DeadlineAsync();
            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = _profileRepository.GetByID(id);
            if (profile == null) return NotFound();
            _profileRepository.Delete(profile);
            await _unitOfWork.DeadlineAsync();
            return NoContent();
        }
    }
}
