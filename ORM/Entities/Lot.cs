using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Lot
    {
        public Lot()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int StartPrice { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime FinishDateTime { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        //public int CurrentBidId { get; set; }

        //public virtual Bid CurrentBid { get; set; }

        // TODO: Добавить, если будет время
        //public RedemptionPrice { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual Category Category { get; set; }

        public bool Sold { get; set; }

        
        public int ModeratorId { get; set; }

        public virtual User Moderator { get; set; }
         
        public int ModerationStatusId { get; set; }
        
        public virtual ModerationStatus ModerationStatus { get; set; }

        // TODO: Задать атрибут на другой тип, который соответствуею DateTime
        [Column(TypeName = "datetime2")]
        public DateTime ModerationDateTime { get; set; }

        [MaxLength(500)]
        public string ModeratorMessage { get; set; }
        
    }
}
