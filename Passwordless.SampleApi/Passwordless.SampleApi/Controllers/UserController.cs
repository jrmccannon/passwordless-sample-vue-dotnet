using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passwordless.Net;

namespace Passwordless.SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IPasswordlessClient passwordlessClient;

    public UserController(ILogger<UserController> logger, IPasswordlessClient passwordlessClient)
    {
        this.logger = logger;
        this.passwordlessClient = passwordlessClient;
    }

    [AllowAnonymous]
    [HttpPost("create-token")]
    public async Task<IActionResult> CreateToken([FromQuery] string user)
    {
        try
        {
            return Ok(await passwordlessClient.CreateRegisterToken(new RegisterOptions
            {
                UserId = Guid.NewGuid().ToString(),
                Username = user,
                Aliases = new HashSet<string> { user }
            }));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occured registering user.");
            return StatusCode(500, ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromQuery] string token)
    {
        try
        {
            return Ok(await passwordlessClient.VerifyToken(token));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to verify user token.");
            return StatusCode(500, ex.Message);
        }
    }
}