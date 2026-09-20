using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Practical_Exam.Data;
using Practical_Exam.Models;

namespace Practical_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbCotnext _context;
        public StudentsController()
        {
            _context = new AppDbCotnext();
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(_context.Students.ToList());
        }
        [HttpGet("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            var data = _context.Students.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [Route("/api/Students/FirstName/{FirstName:alpha}")]
        [HttpGet]
        public IActionResult GetStudentFirtName(string FirstName)
        {
            var data = _context.Students.FirstOrDefault(s => s.FirstName.Contains(FirstName));
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [Route("/api/Students/LastName/{LastName}")]
        [HttpGet]
        public IActionResult GetStudentByLastName(string LastName)
        {
            var data = _context.Students.FirstOrDefault(s => s.LastName == LastName);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = _context.classRooms.Any(c => c.Id == student.ClassroomId);
            if (!data)
            {
                return BadRequest(new { message = "ClassRoom Not Found!" });
            }
            _context.Students.Add(student);
            _context.SaveChanges();

            return Created();
        }
        [HttpPut("{Id}")]
        public IActionResult Update([FromBody] Student student,int Id) { 
        if(Id!= student.Id)
          return BadRequest();
            var data = _context.Students.FirstOrDefault(s => s.Id == Id);
            if(data == null)
                return NotFound();
            _context.Students.Update(student);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{Id},{Name}")]
        public IActionResult UpdateFirstName(int Id,string Name)
        {
            var data = _context.Students.FirstOrDefault(s => s.Id == Id);
            if(data == null)
            {
                return NotFound();
            }
            data.FirstName = Name;
            _context.Students.Update(data);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id) {
            var data = _context.Students.FirstOrDefault(s => s.Id == Id);
            if (data == null)
            {
                return NotFound();
            }
            _context.Students.Remove(data);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
