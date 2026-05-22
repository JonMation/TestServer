using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var data = new List<Item>
{
    new Item(1, "Test 1"),
    new Item(2, "Test 2")
};

app.MapGet("/data", () =>
{
    Console.WriteLine("Data:" + data.Count);
    return Results.Ok(data);
});

app.MapPost("/data", (Item item) =>
{
    Console.WriteLine("Data:" + data.Count);
    data.Add(item);
    return Results.Created($"/data/{item.Id}", item);
});

app.MapDelete("/data/{id:int}", (int id) =>
{
    Console.WriteLine("Data:" + data.Count);
    var item = data.FirstOrDefault(x => x.Id == id);

    if (item == null)
        return Results.NotFound();

    data.Remove(item);
    return Results.NoContent();
});

app.Run();

record Item(int Id, string Name);