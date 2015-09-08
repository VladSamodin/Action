using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllBid : IEntity
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public int UserId { get; set; }

        public int Sum { get; set; }

        public DateTime DateTime { get; set; }
    }
}
