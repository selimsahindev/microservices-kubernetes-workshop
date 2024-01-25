using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

if (env.IsProduction())
{
    Console.WriteLine("--> Using Sql Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseMySql(
            builder.Configuration.GetConnectionString("PlatformsConnection"),
            new MySqlServerVersion(new Version(8, 0, 26)))
    );
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

// Add services to the container.
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();

Console.WriteLine($"CommandService Endpoint: {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Seed the database.
PrepDb.PrepPopulation(app, env.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
