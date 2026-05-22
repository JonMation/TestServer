using Microsoft.EntityFrameworkCore;

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

app.MapGet("/data", async (AppDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapPost("/data", async (Item item, AppDbContext db) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();

    return Results.Created($"/data/{item.Id}", item);
});

app.MapDelete("/data/{id:int}", async (int id, AppDbContext db) =>
{
    var item = await db.Items.FindAsync(id);

    if (item == null)
        return Results.NotFound();

    db.Items.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();
}

class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}