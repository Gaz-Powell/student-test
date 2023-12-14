using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Students.DataProvider;
using University.Students.Models;
using University.Students.Web.Models;
using University.Students.Web.Services;

namespace University.Students.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService ?? throw new ArgumentNullException(nameof(studentsService));
        }

        [HttpGet("[controller]")]
        public async Task<IActionResult> Index()
        {
            var students = await _studentsService.GetStudentsAsync();

            var model = new StudentsViewModel
            {
                Students = students
            };

            return View(model);
        }

        [HttpGet("[controller]/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentsService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Unable to find student with ID {id}");
            }

            var yearOfStudyOptions = _studentsService.GetYearOfStudyOptions();

            var model = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                YearOfStudy = student.YearOfStudy,
                YearOfStudyOptions = yearOfStudyOptions,
                Subjects = student.Subjects
            };

            return View(model);
        }

        [HttpGet("[controller]/create")]
        public async Task<IActionResult> Create()
        {
            var yearOfStudyOptions = _studentsService.GetYearOfStudyOptions();

            var model = new StudentViewModel
            {
                YearOfStudyOptions = yearOfStudyOptions
            };

            return View(model);
        }

        [HttpPost("[controller]/create")]
        public async Task<IActionResult> Create(StudentViewModel request)
        {
            if (request == null)
            {
                return BadRequest("Create student request was null");
            }
            
            var newStudentId = await _studentsService.CreateStudentAsync(request);

            var newStudent = await _studentsService.GetStudentByIdAsync(newStudentId);

            if (newStudent == null)
            {
                return View("Error");
            }

            return RedirectToAction("Details", "Students", new { id = newStudentId });
        }

        [HttpPost("[controller]/update")]
        public async Task<IActionResult> Update(StudentViewModel request)
        {
            await _studentsService.UpdateStudentAsync(request);

            return RedirectToAction("Details", "Students", new { id = request.Id });
        }

        [HttpGet("[controller]/{id:int}/confirm-delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var student = await _studentsService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Unable to find student with ID {id}");
            }

            var yearOfStudyOptions = _studentsService.GetYearOfStudyOptions();

            var model = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                YearOfStudy= student.YearOfStudy,
                YearOfStudyOptions= yearOfStudyOptions
            };

            return View(model);
        }

        [HttpPost("[controller]/delete")]
        public async Task<IActionResult> Delete(StudentViewModel model)
        {
            await _studentsService.DeleteStudentAsync(model.Id);

            return RedirectToAction("Index");
        }
    }
}
