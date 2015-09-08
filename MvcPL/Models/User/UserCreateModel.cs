using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.User
{
    public class UserCreateModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(3, ErrorMessage = "Минимальная длина поля 3 символов")]
        [MaxLength(30, ErrorMessage = "Максимальная длина поля 30 символов")]
        public string Name { get; set; }

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

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public Role Role { get; set; }
    }
}