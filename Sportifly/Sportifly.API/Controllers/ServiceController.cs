using Microsoft.AspNetCore.Mvc;
using Sportifly.API.Interface;
using Sportifly.API.Model;

namespace Sportifly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private IServiceRepository serviceRepository;

        public ServiceController (IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        [HttpGet]
        public IActionResult  GetServiceAll()
        {
            return Ok(serviceRepository.GetServices());
        }
    }
}
