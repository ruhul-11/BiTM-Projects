using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class CourseAssign
    {
        public int CourseAssignId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Display(Name = "Credit to be Taken")]
        [Editable(false)]
        [DefaultValue(0.0)]
        public double CreditTaken { get; set; }

        [Display(Name = "Remaining Credit")]
        [Editable(false)]
        [DefaultValue(0.0)]
        public double CreditLeft { get; set; }

        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Name { get; set; }

        public double Credit { get; set; }
    }
}