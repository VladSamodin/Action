using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class Category
    {
        public Category()
        {
            Lots = new HashSet<Lot>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
    }
}
