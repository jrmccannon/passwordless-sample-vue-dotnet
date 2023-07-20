namespace Passwordless.SampleApi.Models;

public class Penguin
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public string ScientificName { get; init; }
    public IEnumerable<string> FavoriteFood { get; init; }
}