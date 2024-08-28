using Ipet.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Ipet.ViewModels
{
    public class FavoritoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }

        //Aditional changes
        public ProdutoViewModel Produto { get; set; }
    }
}
