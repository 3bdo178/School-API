using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
        [Range(1,100)]
        public decimal Grade { get; set; }
        public Student Student { get; set; } =new Student();
        public Subject Subject { get; set; } = new Subject();
    }
}
