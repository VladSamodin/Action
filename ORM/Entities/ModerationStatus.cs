﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class ModerationStatus
    {
        public ModerationStatus()
        {
            Lots = new HashSet<Lot>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
        /*
        public static bool operator ==(ModerationStatus lhs, ModerationStatus rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (Equals(lhs, null) || Equals(rhs, null))
                return false;
            return lhs.Id == rhs.Id && lhs.Name == rhs.Name;
        }

        public static bool operator !=(ModerationStatus lhs, ModerationStatus rhs)
        {
            return !(lhs == rhs);
        }
        */
    }

}
