using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
