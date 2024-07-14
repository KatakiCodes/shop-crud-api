using System.ComponentModel.DataAnnotations;
namespace Shop_API_Baltaio.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caractéres")]
        [MaxLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caractéres")]
        public string Name { get; set; }
        [MaxLength(1024,ErrorMessage = "Este campo deve conter no máximo 1024 caractéres")]
        public string Description { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}