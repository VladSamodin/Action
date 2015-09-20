using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllLot : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StartPrice { get; set; }

        public int CurrentPrice { get; set; }

        // TODO: Добавить, если будет время
        //public RedemptionPrice { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime FinishDateTime { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }

        public int OwnerId { get; set; }

        public bool Sold { get; set; }

        public int ModeratorId { get; set; }

        public int ModerationStatusId { get; set; }

        public DateTime ModerationDateTime { get; set; }

        public string ModeratorMessage { get; set; }
    }
}
