using System;
using System.ComponentModel.DataAnnotations;
using UserTasksService.IncomingModels;

namespace UserTasksService.Models
{
    public class IncomingUserTask
    {
        [Required]
        [CorrectTaskNumber]
        public string TaskNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range((double)TaskStatus.New, (double)TaskStatus.Done)]
        public TaskStatus Status { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }
    }
}
