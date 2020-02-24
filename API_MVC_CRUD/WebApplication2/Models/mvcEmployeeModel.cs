using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class mvcEmployeeModel
    {
        public int EMPLOYEEID { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string NAME { get; set; }
        public string POSITION { get; set; }
        public Nullable<int> AGE { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Nullable<int> SALARY { get; set; }
        public string ROLENAME { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Username is required")]
        public string USERNAME { get; set; }
        [Display(Name = "User Password")]
        [Required(ErrorMessage = "Password is required")]
        public string USERPASSWORD { get; set; }
    }
}