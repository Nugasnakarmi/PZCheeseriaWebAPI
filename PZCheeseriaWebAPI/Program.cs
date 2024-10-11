using PZCheeseriaWebAPI.Interfaces;
using PZCheeseriaWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";
//Allow specific origin to bypass CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICheeseService, CheeseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);
app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();
