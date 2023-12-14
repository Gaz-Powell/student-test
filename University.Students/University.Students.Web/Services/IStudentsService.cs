using Microsoft.AspNetCore.Mvc.Rendering;
using University.Students.Models;
using University.Students.Web.Models;

namespace University.Students.Web.Services
{
    public interface IStudentsService
    {
        Task<int> CreateStudentAsync(StudentViewModel request);
        Task DeleteStudentAsync(int id);
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<StudentViewModel>> GetStudentsAsync();
        SelectList GetYearOfStudyOptions();
        Task UpdateStudentAsync(StudentViewModel request);
    }
}