namespace Ipet.Domain.Models
{
    public class Avaliacao : Entity
    {
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public double Estrelas {  get; set; }

        //Aditional changes
        public Guid UserId { get; set; }
    }
}
