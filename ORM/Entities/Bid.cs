using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
    public partial class Bid
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public int UserId { get; set; }

        public int Sum { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual User User { get; set; }
    }
}
