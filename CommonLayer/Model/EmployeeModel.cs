using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string Notes { get; set; }


    }
    public enum Department
    {
        HR,
        Finance,
        Engineer,
        Sales,
        Others
    }
}
