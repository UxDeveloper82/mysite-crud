using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Please Enter UserName")]
        [StringLength(8, MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
