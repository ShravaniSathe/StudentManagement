using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using StudentManagement.Models;
using StudentManagement.Repositories.Abstraction;

namespace StudentManagement.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string connectionString = WebConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            Course = reader["Course"].ToString(),
                            CV = reader["CV"].ToString()
                        });
                    }
                }
            }
            return students;
        }

        public Student GetStudentById(int id) // Get student by ID
        {
            Student student = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            Course = reader["Course"].ToString(),
                            CV = reader["CV"].ToString()
                        };
                    }
                }
            }
            return student;
        }

        public void InsertStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (Name, Gender, Mobile, Email, Course, CV) VALUES (@Name, @Gender, @Mobile, @Email, @Course, @CV)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Gender", student.Gender);
                command.Parameters.AddWithValue("@Mobile", student.Mobile);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@Course", student.Course);
                command.Parameters.AddWithValue("@CV", student.CV);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET Name = @Name, Gender = @Gender, Mobile = @Mobile, Email = @Email, Course = @Course, CV = @CV WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Gender", student.Gender);
                command.Parameters.AddWithValue("@Mobile", student.Mobile);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@Course", student.Course);
                command.Parameters.AddWithValue("@CV", student.CV);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No student record found to update.");
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No student record found to delete.");
                }
            }
        }
    }
}
