namespace LegendaryGuacamole.ConsoleApp;

public static class CollectionsExtensions
{
    public static void ToPage<T>(this IEnumerable<T> items, int pageSize, Action<List<T>> print)
    {
        var currentPage = 1;
        var total = items.Count();
        var pageCount = (int)Math.Ceiling((decimal)total / pageSize);

        string? input = null;

        do
        {
            if (pageCount > 1)
                Console.Clear();

            print(items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList());

            if (pageCount > 1)
            {
                Console.WriteLine($"{total} élément{(total >= 2 ? "s" : "")} - Page {currentPage} / {pageCount} - Aller à la page :");
                input = pageCount > 1 ? Console.ReadLine() : null;
            }
            else
            {
                Console.WriteLine($"{total} élément{(total >= 2 ? "s" : "")}");
            }
        } while (input != null && int.TryParse(input, out currentPage) && currentPage > 0 && currentPage <= pageCount);
    }
}
