using System.ComponentModel.DataAnnotations;
namespace Shop_API_Baltaio.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60,ErrorMessage = "Este campo deve conter no máximo 60 caratéres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caratéres")]
        public string Title { get; set; }
    }
}