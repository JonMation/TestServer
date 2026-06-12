using combit.Reporting;
using combit.Reporting.DataProviders;
using combit.Reporting.Dom;
using System.Data;

DataTable table = new DataTable("Texts");
table.Columns.Add("Text", typeof(string));
table.Rows.Add("Hello World");

string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, @"..\..\..\")
);

string reportsFolder = Path.Combine(projectFolder, "reports");
Directory.CreateDirectory(reportsFolder);

string projectFile = Path.Combine(reportsFolder, "hello.lst");
string outputPdf = Path.Combine(reportsFolder, "hello.pdf");

using ListLabel ll = new ListLabel();
ll.DataSource = new AdoDataProvider(table);

// 1. hello.lst automatisch per Code erstellen

ProjectList project = new ProjectList(ll);

project.Open(
    projectFile,
    LlDomFileMode.Create,
    LlDomAccessMode.ReadWrite
);

ObjectText textObject = new ObjectText(project.Objects);
textObject.Name = "HelloWorldText";
textObject.Position.Set(20000, 20000, 120000, 20000);

Paragraph paragraph = new Paragraph(textObject.Paragraphs);
paragraph.Contents = "\"Hello World das funktiniert super\"";

project.Save();
project.Close();

Console.WriteLine(".lst erstellt:");
Console.WriteLine(projectFile);

// if (!File.Exists(projectFile)) { }

// 2. PDF aus der automatisch erstellten .lst erzeugen
var exportConfig = new ExportConfiguration(
    LlExportTarget.Pdf,
    outputPdf,
    projectFile
);

ll.Export(exportConfig);

Console.WriteLine(".pdf erstellt:");
Console.WriteLine(outputPdf);

Console.ReadLine();