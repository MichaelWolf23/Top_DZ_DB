using Top_DZ1_DB.Models;
using Top_DZ1_DB.Repositories;

namespace Top_DZ1_DB;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=students.db";

        StudentRepository repository = new(connectionString);

        List<Student> students = repository.GetAll();

        Console.WriteLine($"{"ID",-5}{"FirstName",-15}{"LastName",-15}{"Email",-25}");

        Console.WriteLine(new string('-', 65));

        foreach (Student student in students)
        {
            string email = student.Email ?? "—";

            Console.WriteLine($"{student.Id,-5}{student.FirstName,-15}{student.LastName,-15}{email,-25}");
        }
    }
}