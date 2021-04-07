using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInfo.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
        
        public string Note  { get; set; }
        [Required]
        public double Salary { get; set; }
    }
}
