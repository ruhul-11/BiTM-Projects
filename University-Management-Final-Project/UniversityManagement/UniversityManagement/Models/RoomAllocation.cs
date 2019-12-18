using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagement.Models
{
    public class RoomAllocation
    {
        public int RoomAllocationId { get; set; }

        [Required(ErrorMessage = "The Field Department is Required")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "The Field Course is Required")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required(ErrorMessage = "The Field Room is Required")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required(ErrorMessage = "The Field Day is Required")]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        [Required(ErrorMessage = "The Field Start Time is Required")]
        public double StartTime { get; set; }

        [Required(ErrorMessage = "The Field End Time is Required")]
        public double EndTime { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string RoomStatus { get; set; }
    }
}