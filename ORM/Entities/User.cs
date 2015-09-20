namespace ORM.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Bids = new HashSet<Bid>();
            Lots = new HashSet<Lot>();
            Roles = new List<Role>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(5), MaxLength(30)]
        public string Email { get; set; }

        [Required]
        [MinLength(5), MaxLength(30)]
        public string Password { get; set; }

        /*
        [Required]
        public int RoleId { get; set; }
         

        public virtual Role Role { get; set; }
        */

        public virtual ICollection<Role> Roles { get; set; }
        

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
    }
    
}
