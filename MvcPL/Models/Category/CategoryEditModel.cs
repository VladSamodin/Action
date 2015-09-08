using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Category
{
    public class CategoryEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(3, ErrorMessage = "Имя категории не может состоять менее чем из 3-х символов")]
        [MaxLength(50, ErrorMessage = "Имя категории не может состоять более чем из 50 символов")]
        public string Name { get; set; }
    }
}