using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserTasksService.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
