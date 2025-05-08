using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    public class StudentService : IStudentService
    {
        static List<Student> students = new List<Student>
        {
            new Student(){ Id = 1, Name = "Băng", Phone = "0394756477" },
            new Student(){ Id = 2, Name = "Hà", Phone = "0948376488" }
        };

        public List<Student> GetStudents()
        {
            return students;
        }
        public List<Student> GetAllStudents()
        {
            return GetStudents();
        }
    }
}
