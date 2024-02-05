using UglyToad.PdfPig.Content;

public class ReaderState
{
    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public string? Details { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Balance { get; set; }
    
    public void Reset()
    {
        Day = null;
        Month = null;
        Year = null;
        Details = null;
        Amount = null;
        Balance = null;
    }
    
    public bool IsComplete()
    {
        return Day.HasValue && Month.HasValue && Year.HasValue && Details != null && Amount.HasValue && Balance.HasValue;
    }
    
    public Transaction GetTransaction()
    {
        if (!IsComplete())
        {
            throw new Exception("Cannot get transaction when state is not complete.");
        }
        return new Transaction(new DateOnly(Year.Value, Month.Value, Day.Value), Details, Amount.Value, Balance.Value);
    }

    public void ReadNext(Word word)
    {
        var text = word.Text;
        switch (this)
        {
            case { Amount: not null }:
            {
                if (text.StartsWith("-£") || text.StartsWith("£"))
                {
                    if (decimal.TryParse(text.Replace("£", ""), out var balance))
                    {
                        Balance = balance;
                    }
                }


                if (!Balance.HasValue)
                {
                    Reset();
                }

                return;
            }

            case { Year: not null }:
            {
                if (text.StartsWith("-£") || text.StartsWith("+£"))
                {
                    if (decimal.TryParse(text.Replace("£", ""), out var amount))
                    {
                        Amount = amount;
                    }
                    else
                    {
                        Reset();
                    }
                }
                else
                {
                    Details += text + " ";
                }

                if (string.Equals(Details, "Opening Balance ", StringComparison.InvariantCultureIgnoreCase)
                    || string.Equals(Details, "Closing Balance ", StringComparison.InvariantCultureIgnoreCase))
                {
                    Reset();
                }

                return;
            }

            case { Month: not null }:
            {
                if (text.Length == 4 && int.TryParse(word.Text, out var year) && year >= 2020)
                {
                    Year = year;
                }
                else
                {
                    Reset();
                }

                return;
            }

            case { Day: not null }:
            {
                int? month = text switch
                {
                    "Jan" => 1,
                    "Feb" => 2,
                    "Mar" => 3,
                    "Apr" => 4,
                    "May" => 5,
                    "Jun" => 6,
                    "Jul" => 7,
                    "Aug" => 8,
                    "Sep" => 9,
                    "Oct" => 10,
                    "Nov" => 11,
                    "Dec" => 12,
                    _ => null
                };
                if (month.HasValue)
                {
                    Month = month.Value;
                }
                else
                {
                    Reset();
                }

                return;
            }
            
            case { Day: null }:
            {
                if (text.Length == 2 && int.TryParse(word.Text, out var day) && day <= 31)
                {
                    Day = day;
                }
                else
                {
                    Reset();
                }

                return;
            }
        }
    }
}