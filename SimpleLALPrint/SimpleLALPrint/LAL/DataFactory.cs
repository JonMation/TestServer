using System.Data;

namespace SimpleLALPrint;

public class DataFactory
{
    public static DataTable CreateHelloWorldTable()
    {
        DataTable table = new DataTable("Texts");
        table.Columns.Add("Text", typeof(string));
        table.Rows.Add("Hello World");
        
        return table;
    }

}