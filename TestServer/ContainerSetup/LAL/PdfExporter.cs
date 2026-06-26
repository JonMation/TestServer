using combit.Reporting;

namespace SimpleLALPrint;

public class PdfExporter
{
    // 2. PDF aus der automatisch erstellten .lst erzeugen
    public static void Export(ListLabel ll, string reportsFolder, string fileName)
    {
        string projectFile = Path.Combine(reportsFolder, fileName + ".lst");
        string outputPdf = Path.Combine(reportsFolder, fileName + ".pdf");

        var exportConfig = new ExportConfiguration(
            LlExportTarget.Pdf,
            outputPdf,
            projectFile
        );

        ll.Export(exportConfig);
        
        Console.WriteLine(".pdf erstellt:");
        Console.WriteLine(outputPdf);
    }
}