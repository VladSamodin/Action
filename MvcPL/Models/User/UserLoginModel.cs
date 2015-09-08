using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.User
{
    public class UserLoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [MinLength(5, ErrorMessage = "Минимальная длина поля 5 символов")]
        [MaxLength(30, ErrorMessage = "Максимальная длина поля 30 символов")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(5, ErrorMessage = "Минимальная длина поля 5 символов")]
        [MaxLength(30, ErrorMessage = "Максимальная длина поля 30 символов")]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool Remember { get; set; }
    }
}