using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Bid
{
    public class BidCreateModel
    {
        [Required]
        [Display(Name = "Sum ($)")]
        public int Sum { get; set; }

        public int LotId { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }
    }
}