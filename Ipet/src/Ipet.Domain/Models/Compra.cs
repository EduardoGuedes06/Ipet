namespace Ipet.Domain.Models
{
    public class Compra : Entity
    {
        public Guid UsuarioId { get; set; }
        public float Valor { get; set; }
        public string Status { get; set; }

        //Novas adições
        public DateTime DataCompra { get; set; }
        public string ListaProdutos { get; set; }

    }
}
