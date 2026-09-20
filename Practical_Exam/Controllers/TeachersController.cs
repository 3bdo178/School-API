using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practical_Exam.Data;
using Practical_Exam.Dtos;
using Practical_Exam.Models;

namespace Practical_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly AppDbCotnext _context;
        public TeachersController()
        {
            _context = new AppDbCotnext();
        }
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var Teachers = _context.Teachers.ToList();
            if(Teachers == null || Teachers.Count == 0)
            {
                return NotFound("No Teachers Found");
            }
            var Teachersdto = new List<TeacherDto>();
            foreach (var Teacher in Teachers)
            {
                var dto = new TeacherDto
                {
                    FirstName = Teacher.FirstName,
                    LastName = Teacher.LastName,
                    Email = Teacher.Email,
                    Id = Teacher.Id,
                    PhoneNumber = Teacher.PhoneNumber,
                    Salary = Teacher.Salary,
                    DepartmentName = Teacher.Department.Name
                };
                Teachersdto.Add(dto);
            }
            return Ok(Teachersdto);
        }
        [HttpPost]
        public IActionResult CreateTeacher()
        {

        }
    }
}
