using System.ComponentModel.DataAnnotations;

namespace Ipet.ViewModels
{
    public class CarrinhoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo UsuarioId é obrigatório.")]
        public Guid UsuarioId { get; set; }

        [Display(Name = "Produtos no Carrinho")]
        public List<CarrinhoProdutoViewModel> CarrinhoProdutos { get; set; }

        public bool Comfirma {  get; set; }

    }
}
