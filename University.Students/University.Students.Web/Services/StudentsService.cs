using Microsoft.AspNetCore.Mvc.Rendering;
using University.Students.DataProvider;
using University.Students.Models;
using University.Students.Web.Models;

namespace University.Students.Web.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository ?? throw new ArgumentNullException(nameof(studentsRepository));
        }

        public async Task<List<StudentViewModel>> GetStudentsAsync()
        {
            var students = (await _studentsRepository.GetStudentsAsync()).Select(s => new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth
            }).ToList();

            return students;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentsRepository.GetStudentByIdAsync(id);
        }

        public SelectList GetYearOfStudyOptions()
        {
            return new SelectList(
                new List<SelectListItem> {
                    new SelectListItem { Text = YearOfStudy.FirstYear, Value = YearOfStudy.FirstYear },
                    new SelectListItem { Text = YearOfStudy.SecondYear, Value = YearOfStudy.SecondYear },
                    new SelectListItem { Text = YearOfStudy.ThirdYear, Value = YearOfStudy.ThirdYear },
                },
                "Value", "Text"
            );
        }

        public async Task<int> CreateStudentAsync(StudentViewModel request)
        {
            var studentToCreate = new CreateStudent
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                YearOfStudy = request.YearOfStudy
            };

            return await _studentsRepository.CreateStudentAsync(studentToCreate);
        }

        public async Task UpdateStudentAsync(StudentViewModel request)
        {
            var studentToUpdate = new Student
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                YearOfStudy = request.YearOfStudy
            };

            await _studentsRepository.UpdateStudentAsync(studentToUpdate);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentsRepository.DeleteStudentAsync(id);
        }
    }
}
