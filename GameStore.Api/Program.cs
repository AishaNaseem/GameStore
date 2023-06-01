using GameStore.Api.Entities;
const string GetGameEndpointName="GetGame";
List<Game> games = new(){
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var group=app.MapGroup("/games");
group.MapGet("/", () => games);
group.MapGet("/{id}",(int id)=>
{
Game? game=games.Find(game=>game.Id==id);
if (game is null)
{
    return Results.NotFound();

}
return Results.Ok(game);
})
.WithName(GetGameEndpointName);
 group.MapPost("/",(Game game)=>
 {
    game.Id=games.Max(game=>game.Id)+1;
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName,new {id=game.Id},game);
 }
 );

 group.MapPut("/{id}",(int id,Game updatedGame)=>
 {
Game? existingGame=games.Find(game=>game.Id==id);
if (existingGame is null)
{
    return Results.NotFound();
}
    existingGame.Name=updatedGame.Name;
    existingGame.Genre=updatedGame.Genre;
    existingGame.Price=updatedGame.Price;
    existingGame.ReleaseDate=updatedGame.ReleaseDate;
    existingGame.ImageUri=updatedGame.ImageUri;
    return Results.NoContent();
 } );

group.MapDelete("/{id}",(int id)=>
{
    Game? game=games.Find(game=>game.Id==id);
if (game is not null)
{
 games.Remove(game);
}
return Results.NoContent();
}
);



app.Run();
