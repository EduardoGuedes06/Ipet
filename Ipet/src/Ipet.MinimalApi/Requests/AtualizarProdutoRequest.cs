namespace Ipet.MinimalApi.Requests
{
    internal class AtualizarProdutoRequest 
    {
        public Guid Id { get; set; }
        public Guid EstabelecimentoId { get; set; }

        public string Nome { get; set; }
        public string Estabelecimento { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }

}
