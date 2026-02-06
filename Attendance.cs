using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Attendance Date")]
        public DateTime AttendanceDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public AttendanceStatus Status { get; set; }

        [StringLength(200)]
        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        [Display(Name = "Marked By")]
        [StringLength(100)]
        public string? MarkedBy { get; set; }

        [Display(Name = "Marked At")]
        public DateTime MarkedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public Student? Student { get; set; }
        public Class? Class { get; set; }
    }

    public enum AttendanceStatus
    {
        Present,
        Absent,
        Late,
        Excused
    }
}
