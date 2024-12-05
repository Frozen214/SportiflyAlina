using Microsoft.AspNetCore.Mvc;
using Sportifly.API.Interface;
using Sportifly.API.Model;
using Sportifly.API.Repository;

namespace Sportifly.API.Controllers;

[ApiController]
[Route("api/service")]
public class ServiceController : ControllerBase
{
    private IServiceRepository serviceRepository;

    public ServiceController(IServiceRepository serviceRepository)
    {
        this.serviceRepository = serviceRepository;
    }

    [HttpGet("service")]
    [ProducesResponseType(typeof(List<ServiceModel>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetService()
    {

        var serviceList = await serviceRepository.GetServiceAll();

        return Ok(serviceList);
    }
}
