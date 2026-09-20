using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Dtos
{
    public class CreateTeacherDto
    {
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(20), Phone]
        public string? PhoneNumber { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
