using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var Teachers = _context.Teachers.Include(t=>t.Department).ToList();
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
        public IActionResult CreateTeacher(CreateTeacherDto Teacherdto)
        {
            if(Teacherdto == null|| !ModelState.IsValid)
            {
                return BadRequest("Please enter a valid teacher.");
            }
            int count = 0;
            foreach(var charcter in Teacherdto.FullName)
            {
                if(charcter!=' ')
                    count++;
                else
                {
                    break;
                }
            }
            if (count == Teacherdto.FullName.Length)
                return BadRequest();
            string FirstName = Teacherdto.FullName.Substring(0, count);
            string LastName = Teacherdto.FullName.Substring(++count);
            var teacher = new Teacher
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Teacherdto.Email,
                PhoneNumber = Teacherdto.PhoneNumber,
                DepartmentId = Teacherdto.DepartmentId,
                Salary = Teacherdto.Salary
            };
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return Created();
        }
    }
}
