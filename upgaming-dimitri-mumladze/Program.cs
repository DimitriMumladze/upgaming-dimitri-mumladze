using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "https://upgamingapi.resorter360.ge",
        Description = "Production Server"
    });
});

var app = builder.Build();

// Enable Swagger in all environments (production included)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpGaming API v1");
    c.RoutePrefix = "swagger"; // Access at: https://upgamingapi.resorter360.ge/swagger
    c.DocumentTitle = "UpGaming API Documentation";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();