using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
    }
}
