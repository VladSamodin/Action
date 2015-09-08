using System;
using MvcPL.Models.User;
using MvcPL.Models.Lot;

namespace MvcPL.Models.Bid
{
    public class BidViewModel
    {
        public int Sum { get; set; }

        public DateTime DateTime { get; set; }

        public LotViewModel Lot { get; set; }

        public UserViewModel User { get; set; }
    }
}