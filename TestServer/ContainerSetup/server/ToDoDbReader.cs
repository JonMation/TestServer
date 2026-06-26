using System.Data;
using System.Reflection;
using TestServer;

namespace SimpleLALPrint;

public static class ToDoDbReader
{
    public static DataTable LoadTable(AppDbContext db)
    {
        db.Database.EnsureCreated();

        DataTable table = new DataTable("Items");

        // Header
        PropertyInfo[] properties = typeof(ItemToDo).GetProperties();
        foreach (var prop in properties)
        {
            table.Columns.Add(prop.Name, prop.PropertyType);
        }

        // Contend
        foreach (ItemToDo item in db.Items)
        {
            DataRow row = table.NewRow();
            foreach (var prop in properties)
            {
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            }
            table.Rows.Add(row);
        }

        return table;
    }
}