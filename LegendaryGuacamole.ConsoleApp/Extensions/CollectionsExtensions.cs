namespace LegendaryGuacamole.ConsoleApp;

public static class CollectionsExtensions
{
    public static void ToPage<T>(this IEnumerable<T> items, int pageSize, Action<List<T>> print)
    {
        var currentLine = 0;
        var total = items.Count();
        var isTruncated = pageSize < total;

        while (true)
        {
            if (isTruncated)
            {
                Console.Clear();
                Console.WriteLine($"∧ {currentLine}");
            }

            print(items.Skip(currentLine).Take(pageSize).ToList());

            if (isTruncated)
            {
                Console.WriteLine($"∨ {total - currentLine - pageSize}");
                Console.WriteLine($"{total} élément{(total >= 2 ? "s" : "")}");
                Console.WriteLine("Appuyez sur ESC pour quitter");

                while (true)
                {
                    while (!Console.KeyAvailable)
                        Thread.Sleep(50);

                    var keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        currentLine = Math.Min(currentLine + 1, total - pageSize);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        currentLine = Math.Min(currentLine + pageSize, total - pageSize);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        currentLine = Math.Max(currentLine - 1, 0);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        currentLine = Math.Max(currentLine - pageSize, 0);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine(" ");
                        return;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Beep();
                    }
                }
            }
            else
            {
                Console.WriteLine($"{total} élément{(total >= 2 ? "s" : "")}");
                return;
            }
        }
    }
}
