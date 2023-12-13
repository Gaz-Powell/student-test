using University.Students.Models;

namespace University.Students.DataProvider
{
    public interface IStudentsRepository
    {
        Task<int> CreateStudentAsync(CreateStudent student);
        Task DeleteStudentAsync(int id);
        Task<Student> GetStudentByIdAsync(int id);
        Task<IReadOnlyList<Student>> GetStudentsAsync();
        Task UpdateStudentAsync(Student student);
    }
}