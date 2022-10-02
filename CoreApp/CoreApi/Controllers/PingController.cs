using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// Ping to Api
        /// </summary>
        /// <returns>Ping</returns>
        // GET: api/<PingController>
        [HttpGet]
        public IEnumerable<Ping> Get()
        {
            List<Ping> pings = new List<Ping>();
            pings.Add(new Ping() { Id = 1, Name = "Hello1" });
            pings.Add(new Ping() { Id = 2, Name = "Hello2" });
            pings.Add(new Ping() { Id = 3, Name = "Hello3" });
            pings.Add(new Ping() { Id = 4, Name = "Hello4" });
            return pings.ToList();
        }
    }
}
