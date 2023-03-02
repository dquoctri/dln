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

        /// <summary>
        /// The function returns a list of all profiles in the database
        /// </summary>
        /// <returns>
        /// The GetProfiles method returns a list of profiles.
        /// </returns>
        // GET: api/Profiles
        [HttpGet]
        public IActionResult GetProfiles()
        {
            return Ok(_profileRepository.GetAll());
        }

        // GET: api/Profiles/5
        /// <summary>
        /// This function returns a profile based on the id passed in the url
        /// </summary>
        /// <param name="id">The id of the profile you want to get.</param>
        /// <returns>
        /// The profile object is being returned.
        /// </returns>
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
            IList<int> list = new List<int>();
            ICollection<int> list2 = new LinkedList<int>();
            var profile = _profileRepository.GetByID(id);
            if (profile == null) return NotFound();
            _profileRepository.Delete(profile);
            await _unitOfWork.DeadlineAsync();
            return NoContent();
        }
    }
}
