using Microsoft.Data.Sqlite;
using Top_DZ1_DB.Models;

namespace Top_DZ1_DB.Repositories;

public class StudentRepository(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public List<Student> GetAll()
    {
        List<Student> students = [];

        string sql = @"
            SELECT Id, first_name, last_name, email
            FROM students
            ORDER BY first_name";

        using SqliteConnection connection = new(_connectionString);

        connection.Open();

        using (SqliteCommand command = new(sql, connection)) 
        {
            using SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.IsDBNull(3)
                        ? null
                        : reader.GetString(3)
                };

                students.Add(student);
            }
        }
        return students;
    }
}
