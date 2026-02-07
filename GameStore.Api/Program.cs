using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UsePathBase("/api");

app.MapGamesEndpoints();

app.Run();
