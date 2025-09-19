using CRUD;
using CRUD.Common;
using CRUD.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependency(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.ApplySwagger();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigration();

app.Run();
