using System;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingComp.Models
{
    public class TasksResults
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TaskID { get; set; }

        [Required]
        public int Result { get; set; }

        [Required]
        public DateTime SubmitDate { get; set; }


    }
}