using System;
using System.ComponentModel.DataAnnotations;

namespace UserTasksService.Models
{
    public class UserTask
    {
        public int Id { get; set; }

        [Required]
        public DateTime TaskDate { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TaskStatus Status { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public User User { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
