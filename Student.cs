using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student ID number is required")]
        [StringLength(20)]
        [Display(Name = "Student ID Number")]
        public string StudentIdNumber { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Class/Section")]
        public int ClassId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Class? Class { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }

        // Computed property
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
