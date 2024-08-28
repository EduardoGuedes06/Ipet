using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ipet.MVC.Models;
using Ipet.ViewModels;
using AutoMapper;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Ipet.MVC.Interfaces;
using Ipet.Data.Repository;
using Ipet.Domain.Notificacoes;
using System.Text;
using Ipet.MVC.Extensions;

namespace Ipet.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : BaseController
    {
        private readonly IHttpService _httpService;
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICarrinhoRepository _carrinhoRepo;
        private readonly ICompraRepository _compraRepo;
        private readonly ICarrinhoProdutoRepository _carrinhoProdutoRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public CarrinhoController(ICarrinhoService carrinhoService,
                                  ICompraRepository compraRepo,
                                  INotificador notificador,
                                  ICarrinhoProdutoRepository carrinhoProdutoRepository,
                                  ICarrinhoRepository carrinhoRepo, IHttpService httpService, IMapper mapper, UserManager<ApplicationUser> userManager) : base(notificador)
        {
            _compraRepo = compraRepo;
            _carrinhoService = carrinhoService;
            _carrinhoRepo = carrinhoRepo;
            _userManager = userManager;
            _carrinhoProdutoRepository = carrinhoProdutoRepository;
            _mapper = mapper;
            _httpService = httpService;
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            //var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);

            ViewData["Title"] = "Carrinho";
            if (await _carrinhoRepo.ObterUsuarioPorId(usuarioId) == false)
            {
                var CarrinhoCriado = await _carrinhoService.CriarCarrinho(usuarioId);
                var carrinhoCriadoViewModel = _mapper.Map<CarrinhoViewModel>(CarrinhoCriado);
                return View(carrinhoCriadoViewModel);
            }

            //Obtem Carrinho e Verifica tudo
            var Carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);
            var carrinhoViewModel = _mapper.Map<CarrinhoViewModel>(Carrinho);

            //Botão de compra
            bool confirma = TempData["QuerComprar"] as bool? ?? false;
            if (confirma) { carrinhoViewModel.Comfirma = true; }

            return View(carrinhoViewModel);

        }

        [Route("carrinho/conta")]
        public async Task<IActionResult> Conta()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);

            var carrinhoViewModel = _mapper.Map<CarrinhoViewModel>(carrinho);
            decimal valorDecimal = 0;

            if (carrinhoViewModel.CarrinhoProdutos != null)
            {
                valorDecimal = carrinhoViewModel.CarrinhoProdutos.Sum(item => item.Produto.Valor);
            }
            ViewData["ValorCarrinho"] = valorDecimal;
            return View("_SomaCarrinhoPartial");
        }

        [HttpGet("carrinho/adicionar")]
        public async Task<IActionResult> AdicionarProduto(Guid produtoId, int quantidade)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            await _carrinhoService.AdicionarProduto(usuarioId, produtoId, quantidade);
            return RedirectToAction("Index");
        }

        [HttpPost("carrinho/alterar-quantidade")]
        public async Task<IActionResult> AtualizarQuantidadeProduto(Guid produtoId, int quantidade)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinhoId = await _carrinhoProdutoRepository.ObterCarrinhoIdPorUsuarioId(usuarioId);

            await _carrinhoService.AtualizarQuantidadeProduto(carrinhoId, produtoId, quantidade);
            return RedirectToAction("Index");
        }

        [HttpPost("carrinho/remover")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinhoId = await _carrinhoProdutoRepository.ObterCarrinhoIdPorUsuarioId(usuarioId);

            await _carrinhoService.RemoverProduto(carrinhoId, produtoId);
            return RedirectToAction("Index");
        }

        [Route("CompraCarrinho")]
        public async Task<IActionResult> CompraCarrinho()
        {



            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);
            var compra = await _compraRepo.ObterCompraEmAbertoPorUsuario(usuarioId);

            if (carrinho == null || carrinho.CarrinhoProdutos.Count == 0)
            {
                _notificador.Handle(new Notificacao("Carrinho Vazio"));
                TempData["ErrorMessage"] = "Carrinho vazio";
                return RedirectToAction("Index");
            }

            if (compra != null)
            {
                TempData["ErrorMessage"] = "Ja existe uma compra aguardando Confirmacao.";
                return RedirectToAction("Index");
            }


            TempData["QuerComprar"] = true;

            return RedirectToAction("Index");
        }
        [Route("CancelaCarrinho")]
        public async Task<IActionResult> CancelaCarrinho()
        {

            TempData["QuerComprar"] = false;

            return RedirectToAction("Index");
        }

        [Route("RealizarCompra")]
        public async Task<IActionResult> RealizarCompra()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);
            var compra = await _compraRepo.ObterComprasPorUsuario(usuarioId);
           
            if (await _carrinhoRepo.ObterUsuarioPorId(usuarioId) == false)
            {
                await _carrinhoService.CriarCarrinho(usuarioId);
                return RedirectToAction("Index");
            }

            float valorTotal = (float)carrinho.CarrinhoProdutos.Sum(cp => cp.Produto.Valor * cp.Quantidade);
            StringBuilder detalhesCarrinho = new StringBuilder();

            var x = 1;
            if (carrinho.CarrinhoProdutos.Count != 0)
            {
                foreach (var item in carrinho.CarrinhoProdutos)
                {
                    detalhesCarrinho.AppendLine($"{item.ProdutoId} : {item.Quantidade} {item.Produto.Nome} - R$ {item.Produto.Valor.ToString("F2")};");
                    x++;
                }

                Compra compraCurso = new Compra
                {
                    UsuarioId = usuarioId,
                    ListaProdutos = detalhesCarrinho.ToString(),
                    Valor = valorTotal,
                    Status = "Aguardando Pagamento",
                };

                await _compraRepo.Adicionar(compraCurso);
                foreach (var item in carrinho.CarrinhoProdutos)
                {
                    await _carrinhoProdutoRepository.RemoverProdutosDoCarrinho(item.Produto.Id);
                }

                return RedirectToAction("Index", "Compra");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
