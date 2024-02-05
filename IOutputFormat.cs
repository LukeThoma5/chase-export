public interface IOutputFormat
{
    string Extension { get; }
    string ToOutput(IEnumerable<Transaction> transactions);
}