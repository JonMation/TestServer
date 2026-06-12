using System.Data;
using combit.Reporting;
using combit.Reporting.DataProviders;

namespace SimpleLALPrint;

public class LALManager
{
    public static void Create()
    {
        string fileName = "hello";

        string projectFolder = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, @"..\..\..\")
        );

        string reportsFolder = Path.Combine(projectFolder, "reports");
        Directory.CreateDirectory(reportsFolder);

        string jsonPath = Path.Combine(projectFolder, "data", "items.json");

        DataTable table = JsonDataFactory.Load(jsonPath);
        
        using ListLabel ll = new ListLabel();
        ll.DataSource = new AdoDataProvider(table);

        TemplateCreator.Create(ll, table, reportsFolder, fileName);
        PdfExporter.Export(ll, reportsFolder, fileName);
    }
}