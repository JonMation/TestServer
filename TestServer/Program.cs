var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var data = new List<Item>
{
    new Item(1, "Test 1"),
    new Item(2, "Test 2")
};

app.MapGet("/data", () =>
{
    return Results.Ok(data);
});

app.MapPost("/data", (Item item) =>
{
    data.Add(item);
    return Results.Created($"/data/{item.Id}", item);
});

app.MapDelete("/data/{id:int}", (int id) =>
{
    var item = data.FirstOrDefault(x => x.Id == id);

    if (item == null)
        return Results.NotFound();

    data.Remove(item);
    return Results.NoContent();
});

app.Run();

record Item(int Id, string Name);