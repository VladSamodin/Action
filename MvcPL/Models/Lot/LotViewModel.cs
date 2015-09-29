using System;
using MvcPL.Models.Category;

namespace MvcPL.Models.Lot
{

    public enum ModerationStatus
    {
        Unchecked = 1,
        Checked,
        Invalid 
    }

    public class LotViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CurrentPrice { get; set; }

        public DateTime FinishDateTime { get; set; }

        public byte[] Image { get; set; }

        public CategoryViewModel Category { get; set; }

        public int OwnerId { get; set; }

        //public ModerationStatus ModerationStatus { get; set; }
        public String ModerationStatus { get; set; }

        public String ModerationMessage { get; set; }
    }
}