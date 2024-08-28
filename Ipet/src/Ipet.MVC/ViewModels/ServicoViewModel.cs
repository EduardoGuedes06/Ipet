using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ipet.MVC.Extensions;
using Ipet.Domain.Models;

namespace Ipet.ViewModels
{
    public class ServicoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Estabelecimento")]
        public Guid EstabelecimentoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }
        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        [DisplayName("NomeEstabelecimento")]
        public string Estabelecimento { get; set; }
        public string HashtagsInput { get; set; }
        public List<ServiçoHashtagViewModel> Hashtags { get; set; }
    }
}
