using System.Text;

public class CsvOutputFormat : IOutputFormat
{
    public string Extension => ".csv";
    public string ToOutput(IEnumerable<Transaction> transactions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Date,Details,Amount,Balance");
        foreach (var transaction in transactions)
        {
            sb.AppendLine($"{transaction.Date:yyyy-MM-dd},{transaction.Details},{transaction.Amount},{transaction.Balance}");
        }
        return sb.ToString();
    }
}