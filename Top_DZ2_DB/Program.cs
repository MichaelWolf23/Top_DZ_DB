using Top_DZ2_DB.Models;
using Top_DZ2_DB.Repositories;
using Top_DZ2_DB.Views;

namespace Top_DZ2_DB;

class Program
{
    private static readonly StudentRepository repository = new("Data Source=students.db");
    public static List<Student> Students => repository.GetAll();

    static void Main()
    {

        string[] menuItems = [
            "Показать всех",
            "Добавить студента",
            "Редактировать студента",
            "Удалить запись",
            "Выход"
        ];

        ConsoleMenu mainMenu = new("Главное меню", menuItems);

        bool running = true;
        while (running)
        {
            int choice = mainMenu.Show();

            Console.Clear();
            switch (choice)
            {
                case 0:
                    ShowAllStudentsProcess();
                    break;
                case 1:
                    AddStudentProcess();
                    break;
                case 2:
                    UpdateStudentProcess();
                    break;
                case 3:
                    DeleteStudentProcess();
                    break;
                case 4:
                    running = false;
                    Console.WriteLine("Программа завершена.");
                    continue;
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }

    public static void ShowAllStudentsProcess()
    {
        ConsoleHelper.PrintStudentsTable(Students);
    }

    public static void AddStudentProcess()
    {
        Student student = new()
        {
            FirstName = ConsoleHelper.Input("Введите имя студента", "Ошибка: Вы не ввели имя студента")!,
            LastName = ConsoleHelper.Input("Введите фамилию студента", "Ошибка: Вы не ввели фамилию студента")!,
            Email = ConsoleHelper.Input("Введите email студента (Enter для пустого email)", isRequired: false)
        };
        bool isAdd = repository.Add(student);
        Console.WriteLine(isAdd ? "Студент успешно добавлен!" : "Ошибка при добавлении студента.");
    }

    public static void UpdateStudentProcess()
    {
        var currentStudents = Students;

        var student = StudentSelector.Select(currentStudents, "Редактирование данных");
        if (student is null) return;

        Console.Clear();
        Console.WriteLine($"--- РЕДАКТИРОВАНИЕ: {student.FirstName} {student.LastName} ---");
        Console.WriteLine("Нажмите Enter, чтобы оставить текущее значение.\n");

        string? newName = ConsoleHelper.Input($"Имя ({student.FirstName})", isRequired: false);
        string? newLastName = ConsoleHelper.Input($"Фамилия ({student.LastName})", isRequired: false);

        string? newEmail = ConsoleHelper.Input($"Email [{student.Email ?? "пусто"}] (введите '-' для удаления)", isRequired: false);

        bool isChanged = false;

        if (!string.IsNullOrWhiteSpace(newName)) { student.FirstName = newName; isChanged = true; }
        if (!string.IsNullOrWhiteSpace(newLastName)) { student.LastName = newLastName; isChanged = true; }

        if (newEmail == "-")
        {
            student.Email = null;
            isChanged = true;
        }
        else if (!string.IsNullOrWhiteSpace(newEmail))
        {
            student.Email = newEmail;
            isChanged = true;
        }

        if (isChanged)
        {
            repository.Update(student);
            ConsoleHelper.WriteSuccess("\nДанные успешно обновлены!");
        }
        else
        {
            Console.WriteLine("\nИзменений не внесено.");
        }
    }

    public static void DeleteStudentProcess()
    {
        var student = StudentSelector.Select(Students, "Выберите студента для удаления");
        
        if (student is null) return;
        
        Console.WriteLine($"\nВы уверены, что хотите удалить {student.FirstName}? (y/n)");
        
        if (Console.ReadKey(true).Key == ConsoleKey.Y)
        {
            bool isDelete = repository.Delete(student.Id);
            Console.WriteLine(isDelete ? "Студент успешно удален!" : "Ошибка при удалении студента.");
        }
    }
}