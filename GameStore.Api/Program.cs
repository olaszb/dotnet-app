using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connectionString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.UsePathBase("/api");

app.MapGamesEndpoints();

app.Run();
