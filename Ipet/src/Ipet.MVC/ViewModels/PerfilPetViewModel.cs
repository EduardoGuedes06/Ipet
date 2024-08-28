using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ipet.ViewModels
{

    public class PerfilPetViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string TipoAnimal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Raca { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Porte { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Observacao { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
    }

}
