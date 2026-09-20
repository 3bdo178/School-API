using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
