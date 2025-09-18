
using CRUD.Interfaces;
using CRUD.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using CRUD.Context;
using CRUD.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole().AddDebug();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<DbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option => option.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None));
}

app.UseAuthorization();

app.MapControllers();

app.ApplyMigration();

app.Run();
