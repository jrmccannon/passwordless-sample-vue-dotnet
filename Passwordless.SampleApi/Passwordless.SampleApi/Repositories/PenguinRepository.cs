using Passwordless.SampleApi.Models;

namespace Passwordless.SampleApi.Repositories;

public interface IPenguinRepository
{
    IEnumerable<Penguin> GetAll();
}

public class PenguinRepository : IPenguinRepository
{
    private static readonly IEnumerable<Penguin> _penguins = new List<Penguin>
    {
        new() { Id = Guid.NewGuid(), Name = "Adelie", ScientificName = "Pygoscelis adeliae", FavoriteFood = new List<string> { "krill" } },
        new() { Id = Guid.NewGuid(), Name = "Emperor", ScientificName = "Aptenodytes forsteri", FavoriteFood = new List<string> { "fish", "squid" } },
    };

    public IEnumerable<Penguin> GetAll()
    {
        return _penguins;
    }
}