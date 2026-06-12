using Microsoft.EntityFrameworkCore;
using SimpleLALPrint;

namespace TestServer;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this WebApplication app)
    {
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

        app.MapGet("/data/export", () =>
        {
            Task.Run(() =>
            {
                LALManager.Create();
            });

            return Results.Accepted("/data/export", "PDF export wurde gestartet.");
        });
    }
}