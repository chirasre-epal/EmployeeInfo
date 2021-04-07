using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInfo.Models
{
    public class Login
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
