using Microsoft.EntityFrameworkCore;
using SimpleLALPrint;
using TestServer;

var builder = WebApplication.CreateBuilder(args);

Directory.CreateDirectory("data");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=data/data.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapItemEndpoints();

app.Run();