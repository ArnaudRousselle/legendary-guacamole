using System.CommandLine.Parsing;
using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.ConsoleApp;

public static class Parsers
{
    public static ParseArgument<ShortDate?> ShortDateParser => result =>
    {
        if (result.Tokens.Count == 0)
            return null;
        var value = result.Tokens.Single().Value;
        if (value.Length != 6
            || !int.TryParse(value[..2], out int day)
            || !int.TryParse(value[2..4], out int month)
            || !int.TryParse(value[4..], out int year))
        {
            result.ErrorMessage = "Le format doit être ddmmyyyy";
            return null;
        }
        return new()
        {
            Day = day,
            Month = month,
            Year = year
        };
    };
}