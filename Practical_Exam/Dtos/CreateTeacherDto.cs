using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Practical_Exam.Dtos
{
    public class CreateTeacherDto
    {
        [Required, MaxLength(100)]
       public string FullName { get; set; }
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(20), Phone]
        public string? PhoneNumber { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
