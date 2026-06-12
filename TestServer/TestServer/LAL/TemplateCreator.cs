using System.Data;
using combit.Reporting;
using combit.Reporting.DataProviders;
using combit.Reporting.Dom;

namespace SimpleLALPrint;

public class TemplateCreator
{
    public static void Create(
        ListLabel ll,
        DataTable table,
        string reportsFolder,
        string fileName)
    {
        // 1) Bind the DataTable as a data source. Without this, the field
        //    placeholders in the .lst have nothing to resolve against.
        //    The TableName must be set, because it becomes the field prefix
        //    (Table.Column) and must match the table's TableId below.
        if (string.IsNullOrEmpty(table.TableName))
            table.TableName = "Data";
        
        string projectFile = Path.Combine(reportsFolder, fileName + ".lst");
        
        ProjectList project = new ProjectList(ll);
        project.Open(
            projectFile,
            LlDomFileMode.Create,
            LlDomAccessMode.ReadWrite
        );

        var page = project.Regions[0].Paper.Extent.Get();

        int containerLeft = 20000;
        int containerTop = 20000;
        int containerWidth = page.Width - 2 * containerLeft;
        int containerHeight = page.Height - 2 * containerTop;

        // 2) A report container holds the table object.
        ObjectReportContainer container = new ObjectReportContainer(project.Objects);
        container.Position.Set(containerLeft, containerTop, containerWidth, containerHeight);

        // The table's TableId must match the data source table name.
        SubItemTable tableItem = new SubItemTable(container.SubItems);
        tableItem.TableId = table.TableName;

        // 3) Exactly ONE header line and ONE data line. List & Label repeats
        //    the data line for every row in the data source at print time,
        //    so there is no loop over table.Rows here.
        TableLineHeader headerLine = new TableLineHeader(tableItem.Lines.Header);
        TableLineData dataLine = new TableLineData(tableItem.Lines.Data);

        int columnWidth = containerWidth / Math.Max(table.Columns.Count, 1);
        string columnWidthText = columnWidth.ToString();

        foreach (DataColumn column in table.Columns)
        {
            // Header cell: quoted literal => a static column title.
            TableFieldText headerCell = new TableFieldText(headerLine.Fields);
            headerCell.Contents = $"\"{column.ColumnName}\"";
            headerCell.Width = columnWidthText;
            headerCell.Font.Bold = "True";

            // Data cell: UNQUOTED field reference => filled from the data
            // source, once per row. Syntax is Table.Column.
            TableFieldText dataCell = new TableFieldText(dataLine.Fields);
            dataCell.Contents = $"{table.TableName}.{column.ColumnName}";
            dataCell.Width = columnWidthText;
        }

        project.Save();
        project.Close();

        Console.WriteLine(".lst erstellt:");
        Console.WriteLine(projectFile);
    }
}