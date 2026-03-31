namespace Top_DZ2_DB.Views;

public class ConsoleMenu(string title, string[] items)
{
    private readonly string[] _items = items;
    private readonly string _title = title;
    private int _selectedIndex = 0;

    public int Show()
    {
        Console.CursorVisible = false;
        bool isSelected = false;

        while (!isSelected)
        {
            DrawMenu();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    _selectedIndex = (_selectedIndex == 0)
                        ? _items.Length - 1
                        : _selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedIndex = (_selectedIndex == _items.Length - 1)
                        ? 0
                        : _selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }

        Console.CursorVisible = true;
        return _selectedIndex;
    }

    private void DrawMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"=== {_title.ToUpper()} ===");
        Console.ResetColor();
        Console.WriteLine();

        for (int i = 0; i < _items.Length; i++)
        {
            if (i == _selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"> {_items[i]} ");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {_items[i]} ");
            }
        }
    }
}
