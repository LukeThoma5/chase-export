using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

public static class TransactionExporter
{
    public static List<Transaction> GetTransactions(string path)
    {
        using PdfDocument document = PdfDocument.Open(path);
        var allTransactions = new List<Transaction>();
        foreach (Page page in document.GetPages())
        {
            var transactions = PageParser.GetTransactions(page);
            allTransactions.AddRange(transactions);
        }
        return allTransactions;
    }
}