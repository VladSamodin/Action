using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.User
{
    public enum Role
    {
        Administrator = 1,
        Moderator,
        User,
        Guest     
    }
    
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User name")]
        public string Name { get; set; }

        public string Email { get; set; }
    }
}