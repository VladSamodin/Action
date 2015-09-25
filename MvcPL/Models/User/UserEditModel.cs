using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.User
{
    public class UserEditModel
    {
        [Display(Name = "User name")]
        public string Name { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(5, ErrorMessage = "Минимальная длина поля 5 символов")]
        [MaxLength(30, ErrorMessage = "Максимальная длина поля 30 символов")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }


    }
}