using Passwordless.SampleApi.DTOs;
using Passwordless.SampleApi.Models;

namespace Passwordless.SampleApi.Mappers;

public static class PenguinMapper
{
    public static PenguinDTO ToResponse(this Penguin penguin) => new PenguinDTO
    {
        Name = penguin.Name,
        ScientificName = penguin.ScientificName
    };
}