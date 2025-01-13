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

    public static ParseArgument<Frequence?> NullableFrequenceParser => result =>
    {
        if (result.Tokens.Count == 0)
            return null;
        var value = result.Tokens.Single().Value;
        switch (value)
        {
            case "monthly": return Frequence.Monthly;
            case "bimonthly": return Frequence.Bimonthly;
            case "quaterly": return Frequence.Quaterly;
            case "annual": return Frequence.Annual;
            default:
                {
                    result.ErrorMessage = "La valeur doit être : monthly, bimonthly, quaterly, annual";
                    return Frequence.Monthly;
                }
        }
    };

    public static ParseArgument<Frequence> FrequenceParser => result =>
    {
        if (result.Tokens.Count == 0)
            return Frequence.Monthly;
        var value = result.Tokens.Single().Value;
        switch (value)
        {
            case "monthly": return Frequence.Monthly;
            case "bimonthly": return Frequence.Bimonthly;
            case "quaterly": return Frequence.Quaterly;
            case "annual": return Frequence.Annual;
            default:
                {
                    result.ErrorMessage = "La valeur doit être : monthly, bimonthly, quaterly, annual";
                    return Frequence.Monthly;
                }
        }
    };

    public static ParseArgument<FileInfo> FileInfoParser => result =>
    {
        if (result.Tokens.Count == 0)
        {
            return null!;
        }
        var filePath = result.Tokens.Single().Value;
        if (!File.Exists(filePath))
        {
            result.ErrorMessage = "Le fichier n'existe pas";
            return null!;
        }
        else
            return new FileInfo(filePath);
    };
}