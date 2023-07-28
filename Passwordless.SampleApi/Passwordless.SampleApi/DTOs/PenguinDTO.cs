namespace Passwordless.SampleApi.DTOs;

public class PenguinDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ScientificName { get; set; } = string.Empty;
    public IEnumerable<string> FavoriteFoods { get; set; }  = new List<string>();
}