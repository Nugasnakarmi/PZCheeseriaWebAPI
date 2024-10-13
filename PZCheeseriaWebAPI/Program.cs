using Microsoft.OpenApi.Models;
using PZCheeseriaWebAPI.Interfaces;
using PZCheeseriaWebAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";
//Allow specific origin to bypass CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICheeseService, CheeseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cheeseria API", Version = "v1",
    Description = "An API for proper cheese operations."});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cheese API V1");
    });
}
    app.UseHttpsRedirection();
    app.UseCors(AllowSpecificOrigins);
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();