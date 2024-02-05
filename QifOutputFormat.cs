using System.Text;

public class QifOutputFormat : IOutputFormat
{
    public string Extension => ".qif";
    public string ToOutput(IEnumerable<Transaction> transactions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("!Type:Bank");
        foreach (var transaction in transactions)
        {
            sb.AppendLine($"D{transaction.Date:dd/MM/yyyy}");
            sb.AppendLine($"T{transaction.Amount}");
            sb.AppendLine($"P{transaction.Details}");
            sb.AppendLine("^");
        }
        return sb.ToString();
    }
}