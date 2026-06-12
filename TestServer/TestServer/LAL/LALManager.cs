using System.Data;
using combit.Reporting;
using combit.Reporting.DataProviders;

namespace SimpleLALPrint;

public class LALManager
{
    public static void Create()
    {
        DataTable table = DataFactory.CreateHelloWorldTable();
        string fileName = "hello";

        string projectFolder = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, @"..\..\..\")
        );

        string reportsFolder = Path.Combine(projectFolder, "reports");
        Directory.CreateDirectory(reportsFolder);

        using ListLabel ll = new ListLabel();
        ll.DataSource = new AdoDataProvider(table);

        TemplateCreator.Create(ll, reportsFolder, fileName);
        PdfExporter.Export(ll, reportsFolder, fileName);

        Console.ReadLine();
    }
}