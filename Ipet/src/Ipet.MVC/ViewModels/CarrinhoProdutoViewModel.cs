using System.ComponentModel.DataAnnotations;

namespace Ipet.ViewModels
{
    public class CarrinhoProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Display(Name = "Produto")]
        public ProdutoViewModel Produto { get; set; }

    }
}
