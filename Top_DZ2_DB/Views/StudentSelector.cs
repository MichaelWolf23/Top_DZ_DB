using Top_DZ2_DB.Models;

namespace Top_DZ2_DB.Views;

public static class StudentSelector
{
    public static Student? Select(IEnumerable<Student> students, string title = "Выберите студента для удаления")
    {
        var list = students.ToList();
        if (list.Count == 0) return null;

        int selectedIndex = 0;
        bool selecting = true;
        Console.CursorVisible = false;

        while (selecting)
        {
            Console.Clear();
            Console.WriteLine($"\n--- {title.ToUpper()} ---\n");

            string header = string.Format(ConsoleHelper.rowTemplate, "ID", "Имя", "Фамилия", "Email");
            Console.WriteLine(header);
            ConsoleHelper.PrintLine(header.Length);

            for (int i = 0; i < list.Count; i++)
            {
                var s = list[i];
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(ConsoleHelper.rowTemplate,
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    (s.Email ?? "—").Length > 25 ? $"{s.Email![..22]}..." : s.Email ?? "—");

                Console.ResetColor();
            }

            Console.WriteLine(new string('-', header.Length));
            Console.WriteLine("\n[↑/↓] - Навигация, [Enter] - Удалить, [Esc] - Отмена");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? list.Count - 1 : selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == list.Count - 1) ? 0 : selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    selecting = false;
                    return list[selectedIndex];
                case ConsoleKey.Escape:
                    return null;
            }
        }
        return null;
    }
}
