public static class MainApp
{
    public static void Run(string path, IOutputFormat formatter)
    {
        var inputFile = new FileInfo(path);
        var outputDir = inputFile.Directory.ToString();
        var outputFile = Path.Combine(outputDir, inputFile.Name.Replace(".pdf", formatter.Extension));
        var transactions = TransactionExporter.GetTransactions(path);
        var result = formatter.ToOutput(transactions);
        File.WriteAllText(outputFile, result);
        Console.WriteLine("Written to " + outputFile);
    }
}