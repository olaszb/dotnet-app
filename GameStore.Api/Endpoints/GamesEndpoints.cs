using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Action-Adventure", 29.99M, new DateOnly(1998, 11, 21)), 
    new (3, "Super Mario 64", "Platformer", 24.99M, new DateOnly(1996, 6, 23)),
    new (4, "Final Fantasy VII", "RPG", 39.99M, new DateOnly(1997, 1, 31)), 
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) => {
            var game = games.Find(g => g.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
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

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games.RemoveAt(index);
            
            return Results.NoContent();
        });
    } 
}
