using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;
public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new(){
    new Game(){
        Id =1,
        Name="Street Fighter II",
        Genre="Frighting",
        Price=19.99M,
        ReleaseDate=new DateTime(1991,2,1),
        ImageUri="https://placehold.ca/100"
    },
    new Game(){
        Id =2,
        Name="Final Fantasy",
        Genre="RolePlaying",
        Price=49.99M,
        ReleaseDate=new DateTime(2001,2,1),
        ImageUri="https://placehold.ca/100"
    },
    new Game(){
        Id =3,
        Name="Fifa 22",
        Genre="Sports",
        Price=40.99M,
        ReleaseDate=new DateTime(2022,5,1),
        ImageUri="https://placehold.ca/100"
    }

};

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }
    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
        await Task.CompletedTask;
    }
    public async Task UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
        await Task.CompletedTask;
    }
}