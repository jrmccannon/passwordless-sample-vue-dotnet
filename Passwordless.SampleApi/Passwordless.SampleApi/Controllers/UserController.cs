using Microsoft.AspNetCore.Mvc;
using Passwordless.SampleApi.DTOs;

namespace Passwordless.SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private static readonly HttpClient Client;

    static UserController()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://v4.passwordless.dev/");
        Client.DefaultRequestHeaders.Add("ApiSecret", "");
    }
    
    public UserController(ILogger<UserController> logger)
    {
        this.logger = logger;
    }

    [HttpPost("create-token")]
    public async Task<IActionResult> CreateToken([FromQuery]string user)
    {
        var request = new RegisterUserRequest
        {
            UserId = Guid.NewGuid(),
            UserName = user,
            Aliases = new List<string> { user }
        };

        var response = await Client.PostAsJsonAsync("register/token", request);

        return response.IsSuccessStatusCode
            ? Ok(await response.Content.ReadFromJsonAsync<RegisterUserResponse>())
            : new JsonResult(await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromQuery]string token)
    {
        var request = new VerifyUserRequest(token);

        var response = await Client.PostAsJsonAsync("signin/verify", request);

        return response.IsSuccessStatusCode
            ? Ok(new { LoggedIn = true })
            : new JsonResult(await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }
}