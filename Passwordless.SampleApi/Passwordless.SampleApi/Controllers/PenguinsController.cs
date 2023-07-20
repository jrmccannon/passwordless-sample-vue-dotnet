using Microsoft.AspNetCore.Mvc;
using Passwordless.SampleApi.Mappers;
using Passwordless.SampleApi.Repositories;

namespace Passwordless.SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PenguinsController : ControllerBase
{
    private readonly ILogger<PenguinsController> logger;
    private readonly IPenguinRepository repository;

    public PenguinsController(ILogger<PenguinsController> logger, IPenguinRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    [HttpGet]
    public IActionResult GetPenguins()
    {
        return Ok(repository.GetAll().Select(p => p.ToResponse()));
    }
}