using System.ComponentModel.DataAnnotations;
using Ipet.Domain.Models;

namespace Ipet.ViewModels
{
    public class AvaliacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid UserId { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public double Estrelas { get; set; }

        public bool Null {  get; set; }
    }

}
