using System.Data;
using combit.Reporting;
using combit.Reporting.DataProviders;
using TestServer;

namespace SimpleLALPrint;

public class LALManager
{
    public static void Create(AppDbContext db)
    {
        string fileName = "hello";

        string reportsFolder = Path.Combine(AppContext.BaseDirectory, "reports");
        Directory.CreateDirectory(reportsFolder);
        
        DataTable table = ToDoDbReader.LoadTable(db);
        
        using ListLabel ll = new ListLabel();
        ll.DataSource = new AdoDataProvider(table);

        TemplateCreator.Create(ll, reportsFolder, fileName);
        PdfExporter.Export(ll, reportsFolder, fileName);
    }
}