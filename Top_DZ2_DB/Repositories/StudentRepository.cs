using Microsoft.Data.Sqlite;
using Top_DZ2_DB.Models;

namespace Top_DZ2_DB.Repositories;

public class StudentRepository(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public List<Student> GetAll()
    {
        List<Student> students = [];

        string sql = """
            
            SELECT Id, first_name, last_name, email
            FROM students
            ORDER BY first_name;
            
            """;

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

    public bool Add(Student student)
    {
        string sql = """
            
            INSERT INTO students (first_name, last_name, email) VALUES
            ($firstName, $lastName, $email);
            
            """;

        using SqliteConnection connection = new(_connectionString);

        connection.Open();

        using SqliteCommand command = new(sql, connection);

        command.Parameters.AddWithValue("$firstName", student.FirstName);
        command.Parameters.AddWithValue("$lastName", student.LastName);
        command.Parameters.AddWithValue("$email", student.Email ?? (object)DBNull.Value);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public bool Update(Student student)
    {
        string sql = """
            
            UPDATE students
            SET
                first_name = $firstName,
                last_name = $lastName,
                email = $email
            WHERE id = $id;
            
            """;

        using SqliteConnection connection = new(_connectionString);

        connection.Open();

        using SqliteCommand command = new(sql, connection);

        command.Parameters.AddWithValue("$id", student.Id);
        command.Parameters.AddWithValue("$firstName", student.FirstName);
        command.Parameters.AddWithValue("$lastName", student.LastName);
        command.Parameters.AddWithValue("$email", student.Email ?? (object)DBNull.Value);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public bool Delete(int id)
    {
        string sql = """
            
            DELETE FROM students
            WHERE id = $id
            
            """;

        using SqliteConnection connection = new(_connectionString);

        connection.Open();

        using SqliteCommand command = new(sql, connection);

        command.Parameters.AddWithValue("$id", id);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }
}
