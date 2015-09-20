using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Lot
{
    public class LotCreateModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Имя лота не может состоять менее чем из 2-х символов")]
        [MaxLength(30, ErrorMessage = "Имя лота не может состоять более чем из 30 символов")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "Максимвальная длина описания 500 символов")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start price ($)")]
        [Range(1, int.MaxValue)]
        public int StartPrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Finish date time (yyyy-MM-dd HH:MM:SS)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FinishDateTime { get; set; }
        
        [DataType(DataType.Upload)]
        [Display(Name = "Image")]
        public byte[] Image { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    
    }
}