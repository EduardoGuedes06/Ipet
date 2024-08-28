using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Service.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }


        public async Task<List<Produto>> GetProdutosByTags(string[] tags = null)
        {
            if(tags == null)
            {
                return await _produtoRepository.ObterTodos();
            }
            var todosProdutos = await _produtoRepository.ObterProdutosOrdenadosPorAvaliacao();

            var produtos = await _produtoRepository.GetProdutosByTag(tags,todosProdutos);

            return produtos;
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}