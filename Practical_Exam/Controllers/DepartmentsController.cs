using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Client;
using Practical_Exam.Data;
using Practical_Exam.Dtos;
using Practical_Exam.Models;

namespace Practical_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbCotnext _context;
        public DepartmentsController()
        {
            _context = new AppDbCotnext();
        }
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = _context.Departments.ToList();
            if(departments == null || departments.Count == 0)
            {
                return NotFound("No departments found!");
            }
            var deptdtos = new List<DepartmentDto>();
            foreach (var department in departments) {
                var deptdto = new DepartmentDto
                {
                    Name = department.Name,
                    Id = department.Id,
                    Description = department.Description
                };
                deptdtos.Add(deptdto);
            }
            return Ok(deptdtos);
        }
        [HttpGet("{Id}")]
        public IActionResult GetDepartmentId(int Id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == Id);
            if (department == null)
            {
                return NotFound("Id does not exist");
            }
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
            return Ok(departmentDto);
        }
        [HttpPost]
        public IActionResult CreateDepartment(CreateDepartmentDto departmentDto)
        {
            if (departmentDto == null || !ModelState.IsValid)
            {
                return BadRequest("Please Enter The Department Correctly");
            }
            Department department = new Department
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };
            _context.Departments.Add(department);
            _context.SaveChanges();
            return Created();
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateDepartment(int Id, CreateDepartmentDto departmentdto)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == Id);
            if(department == null)
            {
                return NotFound("Id does not exist.");
            }
            department.Name = departmentdto.Name;
            department.Description = departmentdto.Description;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
