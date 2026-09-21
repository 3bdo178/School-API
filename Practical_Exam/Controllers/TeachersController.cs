using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_Exam.Data;
using Practical_Exam.Dtos;
using Practical_Exam.Models;
using Practical_Exam.Profiles;

namespace Practical_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly AppDbCotnext _context;
        private readonly IMapper _mapper;
        public TeachersController()
        {
            _context = new AppDbCotnext();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TeacherProfile>();
            });

            _mapper = config.CreateMapper();
        }
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var Teachers = _context.Teachers.Include(t => t.Department).ToList();
            if (Teachers == null || Teachers.Count == 0)
            {
                return NotFound("No Teachers Found");
            }


            var result = _mapper.Map<List<TeacherDto>>(Teachers);
            return Ok(result);
            //var Teachersdto = new List<TeacherDto>();
            //foreach (var Teacher in Teachers)
            //{
            //    var dto = new TeacherDto
            //    {
            //        FirstName = Teacher.FirstName,
            //        LastName = Teacher.LastName,
            //        Email = Teacher.Email,
            //        Id = Teacher.Id,
            //        PhoneNumber = Teacher.PhoneNumber,
            //        Salary = Teacher.Salary,
            //        DepartmentName = Teacher.Department.Name
            //    };
            //    Teachersdto.Add(dto);
            //}
            //return Ok(Teachersdto);
        }
        [HttpGet("{Id}")]
        public IActionResult GetTeacherById(int Id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == Id);
            if (teacher == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<TeacherDto>(teacher);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateTeacher(CreateTeacherDto Teacherdto)
        {
            if (Teacherdto == null || !ModelState.IsValid)
            {
                return BadRequest("Enter a valid teacher.");
            }
            var department = _context.Departments.FirstOrDefault(d => d.Id == Teacherdto.DepartmentId);
            if (department == null)
            {
                return BadRequest("Department does not exist.");
            }
            var result = _mapper.Map<Teacher>(Teacherdto);
            _context.Teachers.Add(result);
            _context.SaveChanges();
            return Created();
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateTeacher(int Id, [FromBody] UpdateTeacherDto dto)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == Id);
            if (teacher == null)
            {
                return BadRequest("Teacher Does not Exist");
            }

            teacher = _mapper.Map<Teacher>(dto);
            _context.Teachers.Update(teacher);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
