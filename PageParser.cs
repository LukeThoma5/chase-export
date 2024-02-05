using UglyToad.PdfPig.Content;

public static class PageParser
{
    public static List<Transaction> GetTransactions(Page page)
    {
        var transactions = new List<Transaction>();
        
        var state = new ReaderState();

        foreach (var word in page.GetWords())
        {
            state.ReadNext(word);
            if (state.IsComplete())
            {
                transactions.Add(state.GetTransaction());
                state.Reset();
            }
        }
        // parse transactions
        return transactions;
    }
}