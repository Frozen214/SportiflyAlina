using Microsoft.AspNetCore.Mvc;
using Sportifly.API.Interface;
using Sportifly.API.Model;

namespace Sportifly.API.Controllers;


[ApiController]
[Route("api/user-service")]
public class UserController : ControllerBase
{
    private IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet("user")]
    [ProducesResponseType(typeof(List<UserModel>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetUser()
    {

        var userList = await userRepository.GetUserAll();

        return Ok(userList);
    }
}
