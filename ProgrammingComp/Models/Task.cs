using System.ComponentModel.DataAnnotations;

namespace ProgrammingComp.Models
{
    public partial class Task
    {
        public int taskID { get; set; }

        [Required]
        [StringLength(50)]
        public string taskName { get; set; }

        [StringLength(500)]
        public string taskDescription { get; set; }

        [StringLength(500)]
        public string taskInputParams { get; set; }

        [StringLength(500)]
        public string taskOutputParams { get; set; }
    }
}
