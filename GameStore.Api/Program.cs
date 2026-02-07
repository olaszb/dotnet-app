using GameStore.Api.Dtos;

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

app.Run();
