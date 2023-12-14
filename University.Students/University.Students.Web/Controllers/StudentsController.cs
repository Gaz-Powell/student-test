using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Unable to find student with ID {id}");
            }

            var yearOfStudyOptions = new SelectList(
                new List<SelectListItem> {
                    new SelectListItem { Text = YearOfStudy.FirstYear, Value = YearOfStudy.FirstYear },
                    new SelectListItem { Text = YearOfStudy.SecondYear, Value = YearOfStudy.SecondYear },
                    new SelectListItem { Text = YearOfStudy.ThirdYear, Value = YearOfStudy.ThirdYear },
                },
                "Value", "Text"
            );

            var model = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                YearOfStudy = student.YearOfStudy,
                YearOfStudyOptions = yearOfStudyOptions
            };

            return View(model);
        }

        [HttpGet("[controller]/create")]
        public async Task<IActionResult> Create()
        {
            var yearOfStudyOptions = new SelectList(
                new List<SelectListItem> {
                    new SelectListItem { Text = YearOfStudy.FirstYear, Value = YearOfStudy.FirstYear },
                    new SelectListItem { Text = YearOfStudy.SecondYear, Value = YearOfStudy.SecondYear },
                    new SelectListItem { Text = YearOfStudy.ThirdYear, Value = YearOfStudy.ThirdYear },
                },
                "Value", "Text"
            );

            var model = new StudentViewModel
            {
                YearOfStudyOptions = yearOfStudyOptions
            };

            return View(model);
        }

        [HttpPost("[controller]/create")]
        public async Task<IActionResult> Create(StudentViewModel request)
        {
            var studentToCreate = new CreateStudent
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                YearOfStudy = request.YearOfStudy
            };

            var newStudentId = await _studentsRepository.CreateStudentAsync(studentToCreate);

            var newStudent = await _studentsRepository.GetStudentByIdAsync(newStudentId);

            if (newStudent == null)
            {
                return View("Error");
            }

            return RedirectToAction("Details", "Students", new { id = newStudentId });
        }

        [HttpPost("[controller]/update")]
        public async Task<IActionResult> Update(StudentViewModel request)
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

            return RedirectToAction("Details", "Students", new { id = request.Id });
        }

        [HttpGet("[controller]/{id:int}/confirm-delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Unable to find student with ID {id}");
            }

            var yearOfStudyOptions = new SelectList(
                new List<SelectListItem> {
                    new SelectListItem { Text = YearOfStudy.FirstYear, Value = YearOfStudy.FirstYear },
                    new SelectListItem { Text = YearOfStudy.SecondYear, Value = YearOfStudy.SecondYear },
                    new SelectListItem { Text = YearOfStudy.ThirdYear, Value = YearOfStudy.ThirdYear },
                },
                "Value", "Text"
            );

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
            await _studentsRepository.DeleteStudentAsync(model.Id);

            return RedirectToAction("Index");
        }
    }
}
