using Microsoft.EntityFrameworkCore;
using TestServer;

var builder = WebApplication.CreateBuilder(args);

//Directory.CreateDirectory("data");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    //LALManager.Create(db);
}

app.MapItemEndpoints();

app.Run();