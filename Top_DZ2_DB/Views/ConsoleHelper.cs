using Top_DZ2_DB.Models;

namespace Top_DZ2_DB.Views;

public static class ConsoleHelper
{
    public const string rowTemplate = "{0,-5} | {1,-15} | {2,-15} | {3,-25}";

    public static void PrintLine(int length) => Console.WriteLine(new string('-', length));

    public static string? Input(string prompt, string errorMessage = "Ошибка: поле не может быть пустым", bool isRequired = true)
    {
        Console.CursorVisible = true;
        while (true)
        {
            Console.Write($"{prompt}: ");
            string? result = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(result) && isRequired)
            {
                WriteError(errorMessage);
                continue;
            }

            Console.CursorVisible = false;
            return string.IsNullOrEmpty(result) ? null : result;
        }
    }

    public static void PrintStudentsTable(IEnumerable<Student> students, string title = "Список студентов")
    {
        if (students?.Any() != true)
        {
            Console.WriteLine($"\n[{title}] — Список пуст.");
            return;
        }

        string header = string.Format(rowTemplate, "ID", "Имя", "Фамилия", "Email");

        Console.WriteLine($"\n--- {title.ToUpper()} ---");
        Console.WriteLine(header);
        PrintLine(header.Length);

        foreach (var s in students)
        {
            Console.WriteLine(rowTemplate,
                s.Id,
                Truncate(s.FirstName, 15),
                Truncate(s.LastName, 15),
                Truncate(s.Email ?? "—", 25));
        }

        PrintLine(header.Length);
        Console.WriteLine($"Всего записей: {students.Count()}\n");
    }

    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static string Truncate(string text, int max) =>
        text.Length <= max ? text : text[..(max - 3)] + "...";
}