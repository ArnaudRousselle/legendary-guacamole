using System.Globalization;
using System.Text;

namespace LegendaryGuacamole.WebApi.Commons;

public static class CreditAgricoleReader
{
    public static DataFromFile ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new Exception("file doesn't exist");

        DataFromFile res = new();

        StringBuilder dataStr = new();
        var dataStarted = false;
        string currentAccountId = string.Empty;
        var creditCardData = false;

        using StreamReader reader = new(File.OpenRead(path), Encoding.UTF8);
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            if (!dataStarted && line.Contains("<ACCTID>"))
            {
                currentAccountId = line.Replace("<ACCTID>", "");
            }

            if (!dataStarted && line.Contains("<CREDITCARDMSGSRSV1>"))
            {
                creditCardData = true;
            }

            if (!dataStarted && line.Contains("</CREDITCARDMSGSRSV1>"))
            {
                creditCardData = false;
            }

            if (line.Contains("<STMTTRN>"))
            {
                dataStarted = true;
            }

            if (dataStarted)
            {
                dataStr.Append(line.Trim());
            }

            if (dataStarted && line.Contains("</STMTTRN>"))
            {
                var str = dataStr.ToString();

                res
                    .Lines
                    .Add(new()
                    {
                        AccountId = currentAccountId,
                        DtPosted =
                                creditCardData
                                ? DateOnly.FromDateTime(DateTime.ParseExact((TryGetValue<string>(str, "<FITID>") ?? "").Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture))
                                : DateOnly.FromDateTime(DateTime.ParseExact(TryGetValue<string>(str, "<DTPOSTED>") ?? "", "yyyyMMdd", CultureInfo.InvariantCulture)),
                        FitId = TryGetValue<string>(str, "<FITID>"),
                        Name = TryGetValue<string>(str, "<NAME>"),
                        Memo = TryGetValue<string>(str, "<MEMO>"),
                        TrnAmt = TryGetValue<decimal>(str, "<TRNAMT>", CultureInfo.InvariantCulture)
                    });

                dataStarted = false;

                dataStr.Clear();
            }
        }

        reader.Close();

        return res;
    }

    private static T? TryGetValue<T>(string sourceStr, string tag, IFormatProvider? formatProvider = null)
    {
        var indexOfTag = sourceStr.IndexOf(tag, StringComparison.Ordinal);

        if (indexOfTag < 0)
            return default;

        var dataStartIndex = indexOfTag + tag.Length;

        var dataEndIndex = sourceStr.IndexOf("<", dataStartIndex, StringComparison.Ordinal);

        if (dataEndIndex < 0)
            return default;

        var data = sourceStr.Substring(dataStartIndex, dataEndIndex - dataStartIndex);

        return (T)Convert.ChangeType(data, typeof(T), formatProvider);
    }

    public class DataFromFile
    {
        public class DataLine
        {
            public DateOnly? DtPosted { get; set; } //date
            public string? FitId { get; set; } //numéro d'opération
            public string? Name { get; set; }
            public string? Memo { get; set; }
            public decimal? TrnAmt { get; set; }//montant
            public string? AccountId { get; set; }

        }

        public string? BankId { get; set; } //code banque
        public string? BranchId { get; set; } //code agence
        public string? AcctId { get; set; } //numéro de compte
        public string? CurDef { get; set; } //devise
        public List<DataLine> Lines { get; set; } = [];
    }
}