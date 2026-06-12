using System.Data;
using System.Text.Json;

namespace SimpleLALPrint;

public static class ToDoJsonReader
{
    public static DataTable Load(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);

        List<ItemToDo>? items =
            JsonSerializer.Deserialize<List<ItemToDo>>(json);

        DataTable table = new DataTable("Items");

        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Name2", typeof(string));
        table.Columns.Add("Amount", typeof(int));

        if (items == null)
            return table;

        foreach (var item in items)
        {
            table.Rows.Add(item.Name, item.Name2, item.Amount);
        }

        return table;
    }
}