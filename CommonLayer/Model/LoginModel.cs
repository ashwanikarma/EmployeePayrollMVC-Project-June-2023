using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace CommonLayer.Model
{
    public class LoginModel
    {
        public int employeeID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Email id is required")]
        [RegularExpression(@"^[a-zA-Z0-9_+&*-]+(?:\\." +
                            "[a-zA-Z0-9_+&*-]+)*@" +
                            "(?:[a-zA-Z0-9-]+\\.)+[a-z" +
                            "A-Z]{2,7}$",ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public int roleID { get; set; }
    }
}
