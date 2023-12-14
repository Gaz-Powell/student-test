using Microsoft.AspNetCore.Mvc;
using University.Students.Web.Services;

namespace University.Students.Web.Controllers
{
    [Route("api/v1/students")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly IStudentsService _studentsService;

        public StudentsApiController(IStudentsService studentsService)
        {
            _studentsService = studentsService ?? throw new ArgumentNullException(nameof(studentsService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentsService.GetStudentsAsync();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _studentsService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}
