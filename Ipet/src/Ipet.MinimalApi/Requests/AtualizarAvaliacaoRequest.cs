using System;

namespace Ipet.MinimalApi.Requests
{
    public class AtualizarAvaliacaoRequest
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public double Estrelas { get; set; }
    }
}
