namespace LegendaryGuacamole.ConsoleApp.Extensions;

public static class StringExtensions
{
    public static string FillRight(this string? str, int count)
    {
        str ??= "";
        if (str.Length == count)
            return str;
        if (str.Length < count)
            return str.PadRight(count, ' ');
        else
            return str[..count];
    }

    public static string FillLeft(this string? str, int count)
    {
        str ??= "";
        if (str.Length == count)
            return str;
        if (str.Length < count)
            return str.PadLeft(count, ' ');
        else
            return str[(count - str.Length)..];
    }
}