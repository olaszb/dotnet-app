using GameStore.Api.Dtos;

const string GetGameEndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UsePathBase("/api");

List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Action-Adventure", 29.99M, new DateOnly(1998, 11, 21)), 
    new (3, "Super Mario 64", "Platformer", 24.99M, new DateOnly(1996, 6, 23)),
    new (4, "Final Fantasy VII", "RPG", 39.99M, new DateOnly(1997, 1, 31)), 
];

// GET /games
app.MapGet("/games", () => games);


// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(g => g.Id == id))
    .WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

app.Run();
