using Journey.Api.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(d =>
{
    d.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NLWJourney.Trips.API",
        Version = "v1",
        Description = "Rocketseat - NLW Journey: API gerenciamento de viagens",
        Contact = new OpenApiContact
        {
            Name = "NLW Journey - Rocketseat",          
        }
    });

    var xmlFile = "Journey.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    d.IncludeXmlComments(xmlPath);
});

builder.Services.AddMvc(config => config.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();