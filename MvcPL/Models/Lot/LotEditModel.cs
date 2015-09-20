using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Lot
{
    public class LotEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Имя лота не может состоять менее чем из 2-х символов")]
        [MaxLength(30, ErrorMessage = "Имя лота не может состоять более чем из 30 символов")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "Максимвальная длина описания 500 символов")]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}