using System.CommandLine.Parsing;
using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.ConsoleApp;

public static class Parsers
{
    public static ParseArgument<ShortDate?> NullableShortDateParser => result =>
    {
        if (result.Tokens.Count == 0)
            return null;
        var value = result.Tokens.Single().Value;
        if (value.Length != 8
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

    public static ParseArgument<ShortDate> ShortDateParser => result =>
    {
        if (result.Tokens.Count == 0)
            return new()
            {
                Day = 1,
                Month = 1,
                Year = 2000
            };
        var value = result.Tokens.Single().Value;
        if (value.Length != 8
            || !int.TryParse(value[..2], out int day)
            || !int.TryParse(value[2..4], out int month)
            || !int.TryParse(value[4..], out int year))
        {
            result.ErrorMessage = "Le format doit être ddmmyyyy";
            return new()
            {
                Day = 1,
                Month = 1,
                Year = 2000
            };
        }
        return new()
        {
            Day = day,
            Month = month,
            Year = year
        };
    };
}