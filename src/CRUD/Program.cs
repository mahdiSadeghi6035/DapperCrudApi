
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option => option.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
