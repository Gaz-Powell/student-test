using Microsoft.AspNetCore.Mvc;
using University.Students.DataProvider;
using University.Students.Models;
using University.Students.Web.Models;

namespace University.Students.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository ?? throw new ArgumentNullException(nameof(studentsRepository));
        }

        [HttpGet("[controller]")]
        public async Task<IActionResult> Index()
        {
            var students = (await _studentsRepository.GetStudentsAsync()).Select(s => new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth
            }).ToList();

            var model = new StudentsViewModel
            {
                Students = students
            };

            return View(model);
        }

        [HttpGet("[controller]/{id:int}")]
        public async Task<IActionResult> Student(int id)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Unable to find student with ID {id}");
            }

            var model = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth
            };

            return View(model);
        }

        [HttpGet("[controller]/create")]
        public async Task<IActionResult> Create()
        {
            var model = new StudentViewModel();

            return View(model);
        }

        [HttpPost("[controller]/create")]
        public async Task<IActionResult> Create(StudentViewModel request)
        {
            var studentToCreate = new CreateStudent
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };

            var newStudentId = await _studentsRepository.CreateStudentAsync(studentToCreate);

            var newStudent = await _studentsRepository.GetStudentByIdAsync(newStudentId);

            if (newStudent == null)
            {
                return View("Error");
            }

            var model = new StudentViewModel
            {
                Id = newStudent.Id,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                DateOfBirth = newStudent.DateOfBirth
            };

            return View("Student", model);
        }

        [HttpPost("[controller]/update")]
        public async Task<IActionResult> Update(StudentViewModel request)
        {
            var studentToUpdate = new Student
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };

            await _studentsRepository.UpdateStudentAsync(studentToUpdate);

            return View("Student", request);
        }

        [HttpGet("[controller]/{id:int}/confirm-delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Unable to find student with ID {id}");
            }

            var model = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth
            };

            return View(model);
        }

        [HttpPost("[controller]/delete")]
        public async Task<IActionResult> Delete(StudentViewModel model)
        {
            await _studentsRepository.DeleteStudentAsync(model.Id);

            return RedirectToAction("Index");
        }
    }
}
