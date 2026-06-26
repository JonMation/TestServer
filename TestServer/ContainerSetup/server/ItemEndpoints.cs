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

        app.MapGet("/data/{id:int}", async (int id, AppDbContext db) =>
        {
            ItemToDo? item = await db.Items.FindAsync(id);

            if (item == null)
                return Results.NotFound();

            return Results.Ok(item);
        });

        app.MapPost("/data", async (ItemToDo item, AppDbContext db) =>
        {
            db.Items.Add(item);
            await db.SaveChangesAsync();

            return Results.Created($"/data/{item.Id}", item);
        });

        app.MapPut("/data/{id:int}", async (int id, ItemToDo updatedItem, AppDbContext db) =>
        {
            ItemToDo? item = await db.Items.FindAsync(id);

            if (item == null)
                return Results.NotFound();

            foreach (var prop in typeof(ItemToDo).GetProperties())
            {
                if (prop.Name == "Id") continue;
                prop.SetValue(item, prop.GetValue(updatedItem));
            }

            await db.SaveChangesAsync();

            return Results.Ok(item);
        });

        app.MapDelete("/data/{id:int}", async (int id, AppDbContext db) =>
        {
            ItemToDo? item = await db.Items.FindAsync(id);

            if (item == null)
                return Results.NotFound();

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapGet("/data/export", (AppDbContext db) =>
        {
            LALManager.Create(db);
        
            return Results.Ok("PDF export wurde erstellt.");
        });
    }
}