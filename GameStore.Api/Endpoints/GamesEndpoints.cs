using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName="GetGame";

    public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
    {
      
var group=routes.MapGroup("/games")
            .WithParameterValidation();

group.MapGet("/", (IGameRepository repository) => repository.GetAll());
group.MapGet("/{id}",(IGameRepository repository,int id)=>
{
Game? game=repository.Get(id);
return game is not null ? Results.Ok(game):Results.NotFound();

})
.WithName(GetGameEndpointName);
 group.MapPost("/",(IGameRepository repository,Game game)=>
 {
    repository.Create(game);
    
    return Results.CreatedAtRoute(GetGameEndpointName,new {id=game.Id},game);
 }
 );

 group.MapPut("/{id}",(IGameRepository repository,int id,Game updatedGame)=>
 {
Game? existingGame=repository.Get(id);
if (existingGame is null)
{
    return Results.NotFound();
}
    existingGame.Name=updatedGame.Name;
    existingGame.Genre=updatedGame.Genre;
    existingGame.Price=updatedGame.Price;
    existingGame.ReleaseDate=updatedGame.ReleaseDate;
    existingGame.ImageUri=updatedGame.ImageUri;
    repository.Update(existingGame);
    return Results.NoContent();
 } );

group.MapDelete("/{id}",(IGameRepository repository,int id)=>
{
    Game? game=repository.Get(id);
if (game is not null)
{
 repository.Delete(id);
}
return Results.NoContent();
});

return group;
    }
}