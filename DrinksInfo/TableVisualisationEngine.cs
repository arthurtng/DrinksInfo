using System.Diagnostics.CodeAnalysis;
using ConsoleTableExt;

namespace DrinksInfo;

public class TableVisualisationEngine
{
    public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
    {
        Console.Clear();
        
        tableName ??= "";
        
        Console.WriteLine("\n\n");
        
        ConsoleTableBuilder
            .From(tableData)
            .WithColumn(tableName)
            .ExportAndWriteLine();
        Console.WriteLine("\n\n");
    }
}