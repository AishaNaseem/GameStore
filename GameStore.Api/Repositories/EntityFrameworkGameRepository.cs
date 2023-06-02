using GameStore.Api.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGameRepository : IGamesRepository
{
    private readonly GameStoreContext dbContext;

    public EntityFrameworkGameRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Game? Get(int id)
    {
        return dbContext.Games.Find(id);
    }

    public IEnumerable<Game> GetAll()
    {
        return dbContext.Games.AsNoTracking().ToList();
    }
    public void Create(Game game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
    }

 public void Update(Game updateGame)
    {
        dbContext.Update(updateGame);
        dbContext.SaveChanges();
    }
    public void Delete(int id)
    {
        dbContext.Games.Where(game=>game.Id==id)
        .ExecuteDelete();
         }

    

   
}