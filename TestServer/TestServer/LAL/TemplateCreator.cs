using System.Data;
using combit.Reporting;
using combit.Reporting.Dom;

namespace SimpleLALPrint;

public class TemplateCreator
{
    public static void Create(
        ListLabel ll,
        string reportsFolder,
        string fileName)
    {
        string projectFile = Path.Combine(reportsFolder, fileName + ".lst");
        
        ProjectList project = new ProjectList(ll);

        project.Open(
            projectFile,
            LlDomFileMode.Create,
            LlDomAccessMode.ReadWrite
        );

        int startX = 20000;
        int startY = 20000;
        int columnWidth = 50000;
        int rowHeight = 15000;

        // Header
        for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
        {
            DataColumn column = table.Columns[columnIndex];

            ObjectText textObject = new ObjectText(project.Objects);
            textObject.Name = $"Header_{column.ColumnName}";
            textObject.Position.Set(
                startX + columnIndex * columnWidth,
                startY,
                columnWidth,
                rowHeight
            );

            Paragraph paragraph = new Paragraph(textObject.Paragraphs);
            paragraph.Contents = $"\"{column.ColumnName}\"";
        }

        // Rows
        for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
        {
            DataRow row = table.Rows[rowIndex];

            for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
            {
                DataColumn column = table.Columns[columnIndex];

                ObjectText textObject = new ObjectText(project.Objects);
                textObject.Name = $"Cell_{rowIndex}_{column.ColumnName}";
                textObject.Position.Set(
                    startX + columnIndex * columnWidth,
                    startY + (rowIndex + 1) * rowHeight,
                    columnWidth,
                    rowHeight
                );

                string value = row[column].ToString() ?? "";

                Paragraph paragraph = new Paragraph(textObject.Paragraphs);
                paragraph.Contents = $"\"{value}\"";
            }
        }

        project.Save();
        project.Close();

        Console.WriteLine(".lst erstellt:");
        Console.WriteLine(projectFile);
    }
}