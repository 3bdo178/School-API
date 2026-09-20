using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Models
{
    public class ClassRoom
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, Range(1,12)]
        public int GradeLevel { get; set; }
        [Required, Range(1,100)]
        public int Capacity { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
