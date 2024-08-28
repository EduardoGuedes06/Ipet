using Ipet.Domain.Models;
using System;

namespace Ipet.MinimalApi.Requests
{
    public class AdicionarAvaliacaoRequest
    {
        public Guid ProdutoId { get; set; }
        public double Estrelas { get; set; }
    }
}
