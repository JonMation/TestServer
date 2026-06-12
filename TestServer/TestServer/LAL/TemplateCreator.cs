using combit.Reporting;
using combit.Reporting.Dom;

namespace SimpleLALPrint;

public class TemplateCreator
{
    // 1. hello.lst automatisch per Code erstellen
    public static void Create(ListLabel ll, string reportsFolder, string fileName)
    {
        string projectFile = Path.Combine(reportsFolder, fileName + ".lst");
        
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
        paragraph.Contents = "\"This is a nother test\"";

        project.Save();
        project.Close();
        
        Console.WriteLine(".lst erstellt:");
        Console.WriteLine(projectFile);
        
        // if (!File.Exists(projectFile)) { }
    }
}