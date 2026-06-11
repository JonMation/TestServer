using combit.Reporting;
using combit.Reporting.DataProviders;
using System.Data;
using System.IO;

DataTable table = new DataTable("Texts");
table.Columns.Add("Text", typeof(string));
table.Rows.Add("Hello World");

using ListLabel LL = new ListLabel();
LL.DataSource = new AdoDataProvider(table);

string projectFolder = AppContext.BaseDirectory;

string outputPdf = Path.Combine(projectFolder, "hello.pdf");
string projectFile = Path.Combine(projectFolder, "hello.lst");

// Designer einmal öffnen
LL.LicensingInfo = "6iZKGg";
LL.Design(LlProject.List, projectFile);

// Danach exportieren
ExportConfiguration config = new ExportConfiguration(
    LlExportTarget.Pdf,
    outputPdf,
    projectFile
);

config.ShowResult = true;

LL.Export(config);

Console.WriteLine("PDF erstellt: " + outputPdf);