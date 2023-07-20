namespace Passwordless.SampleApi.DTOs;

public class GetPenguinsResponse
{
    public IEnumerable<PenguinDTO> Penguins { get; set; } = new List<PenguinDTO>();
}