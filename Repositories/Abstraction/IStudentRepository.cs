using StudentManagement.Models;
using System.Collections.Generic;

namespace StudentManagement.Repositories.Abstraction
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        Student GetStudentById(int id); 
        void InsertStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
