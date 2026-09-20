using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Practical_Exam.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required, Range(1,100)]
        public int MaxGrade { get; set; }
        public int TeacherId { get; set; }
        public Teacherdto Teacher { get; set; } = new Teacherdto();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
