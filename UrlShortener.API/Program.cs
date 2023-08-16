using Microsoft.EntityFrameworkCore;
using UrlShortener.Abstractions.src.UrlShortener;
using UrlShortener.Application.src.UrlShortener;
using UrlShortener.Persistence.src.Contexts;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<MainDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();

builder.Services.AddControllers();

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

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MainDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}
catch (Exception Ex)
{
    logger.LogError(Ex, "A problem occurred during migration!");
}

app.Run();
