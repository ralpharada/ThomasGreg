using System.ComponentModel.DataAnnotations;
using ThomasGreg.Web.Attributes;

namespace ThomasGreg.Web.Models
{
    public class AtualizarClienteFormModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        public string Email { get; set; } = null!;
        [ValidarImagem(ErrorMessage = "Somente imagens são permitidas.")]
        public IFormFile? Logotipo { get; set; }
    }

}
