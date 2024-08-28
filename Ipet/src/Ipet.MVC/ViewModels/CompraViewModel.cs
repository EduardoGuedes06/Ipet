using System.ComponentModel.DataAnnotations;

namespace Ipet.ViewModels
{
    public class CompraViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo UsuarioId é obrigatório.")]
        public Guid UsuarioId { get; set; }

        [Display(Name = "Total")]
        public float Valor { get; set; }

        [Display(Name = "Status da Compra")]
        public string Status { get; set; }

        [Display(Name = "Data")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "DataCompra")]
        public DateTime DataCompra { get; set; }

        public string ListaProdutos { get; set; }

        //External
        public bool Pagou { get; set; }

    }
}
