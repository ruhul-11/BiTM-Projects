using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagement.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Code is Required")]
        [Display(Name = "Code")]
        [Column(TypeName = "varchar")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Course Code Must be 5 characters long")]
        [Remote("IsCodeUnique", "Course", ErrorMessage = "Code already exists!")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Course Name is Reqired")]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar")]
        [Remote("IsNameUnique", "Course", ErrorMessage = "Code already exists!")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Credit")]
        [Range(0.5, 5.0, ErrorMessage = "Credit must be within 0.5 to 5.0")]
        public double CourseCredit { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string CourseDescription { get; set; }

        public string CourseAssignTo { get; set; }

        public bool CourseStatus { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]

        public virtual Department Department { get; set; }

        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]

        public virtual Semester Semester { get; set; }


    }
}