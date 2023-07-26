using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passwordless.Net;
using Passwordless.SampleApi.Auth;
using Passwordless.SampleApi.Models;

namespace Passwordless.SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IPasswordlessClient passwordlessClient;
    private readonly ITokenManager tokenManager;

    public UserController(ILogger<UserController> logger, IPasswordlessClient passwordlessClient,
        ITokenManager tokenManager)
    {
        this.logger = logger;
        this.passwordlessClient = passwordlessClient;
        this.tokenManager = tokenManager;
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
            var response = await passwordlessClient.VerifyToken(token);

            if (response is { Success: true })
            {
                var jwt = tokenManager.GenerateToken(new User
                {
                    Id = response.UserId
                });
                
                return Ok(new { LoggedIn = true, Token = jwt });
            }
            
            logger.LogError("Response was invalid {response}", response);
            return StatusCode(500, "Something unexpected happened.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to verify user token.");
            return StatusCode(500, ex.Message);
        }
    }
}