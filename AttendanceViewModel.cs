using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentIdNumber { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Remarks { get; set; }
    }

    public class MarkAttendanceViewModel
    {
        [Required]
        [Display(Name = "Class")]
        public int ClassId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime AttendanceDate { get; set; } = DateTime.Today;

        public List<AttendanceViewModel> Students { get; set; } = new List<AttendanceViewModel>();

        public string ClassName { get; set; }
    }

    public class AttendanceReportViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentIdNumber { get; set; }
        public string ClassName { get; set; }
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LateDays { get; set; }
        public int ExcusedDays { get; set; }
        public decimal AttendancePercentage { get; set; }
    }
}
