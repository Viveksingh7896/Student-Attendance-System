using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [StringLength(10)]
        [Display(Name = "Section")]
        public string? Section { get; set; }

        [StringLength(100)]
        [Display(Name = "Class Teacher")]
        public string? ClassTeacher { get; set; }

        [Display(Name = "Academic Year")]
        [StringLength(20)]
        public string? AcademicYear { get; set; }

        // Navigation property
        public ICollection<Student>? Students { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }

        // Computed property
        [Display(Name = "Full Class Name")]
        public string FullClassName => Section != null ? $"{ClassName} - {Section}" : ClassName;
    }
}
