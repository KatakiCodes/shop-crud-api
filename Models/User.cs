using System.ComponentModel.DataAnnotations;

namespace Shop_API_Baltaio.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caractéres")]
        [MaxLength(50,ErrorMessage = "Este campo deve conter no máximo 50 caractéres")]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MinLength(3,ErrorMessage = "Este campo deve conter no mínimo 3 caractéres")]
        [MaxLength(30,ErrorMessage = "Este campo deve conter no máximo 30 caractéres")]
        public string Role { get; set; }
    }
}