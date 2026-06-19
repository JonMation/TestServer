using System.Data;
using TestServer;

namespace SimpleLALPrint;

public static class ToDoDbReader
{
    public static DataTable LoadTable(AppDbContext db)
    {
        db.Database.EnsureCreated();

        DataTable table = new DataTable("Items");

        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Name2", typeof(string));
        table.Columns.Add("Amount", typeof(int));

        foreach (ItemToDo item in db.Items)
        {
            table.Rows.Add(
                item.Id,
                item.Name,
                item.Name2,
                item.Amount
            );
        }

        return table;
    }
}