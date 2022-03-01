using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OauthServerCenter.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "邮箱地址")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { set; get; }

        public string ReturnUrl { set; get; }
    }
}
