using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityManagement.Models
{
	//Department Class
    public class Department
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Code is Required")]
        [Display(Name = "Code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Departmrnt Code Must be 2 to 7 characters long")]
        //[Remote("IsDeptCodeExist", "Department", ErrorMessage = "Code already exists!")]
        public string Code { get; set; }


        [Required(ErrorMessage = "Department Name is Required")]
        [Display(Name = "Name")]
        //[Remote("IsDeptNameExist", "Department", ErrorMessage = "Name already exists!")]
        public string Name { get; set; }
    }
}