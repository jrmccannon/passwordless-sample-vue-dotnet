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
        var penguins = repository.GetAll().ToList();

        if (!penguins.Any())
        {
            return NoContent();
        }

        return Ok(penguins.Select(x => x.ToResponse()));
    }
}