
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ipet.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Ipet.MVC.Models;
using Ipet.ViewModels;
using Ipet.Domain.Intefaces;
using Ipet.MVC.Extensions;
using Ipet.MVC.Interfaces;
using Ipet.Data.Repository;

namespace Ipet.MVC.Controllers
{
    [Authorize]
    public class ProdutosController : BaseController
    {
        private readonly ICarrinhoProdutoRepository _carrinhoProdutoRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IProdutoRepository _produtoRepository;

        private readonly IMapper _mapper;
        private readonly IProdutoService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpService _httpService;

        public ProdutosController(
        ICompraRepository compraRepository,
        IProdutoRepository produtoRepository,
        IAvaliacaoRepository avaliacaoRepository,
        IFavoritoRepository favoritoRepository,
        ICarrinhoProdutoRepository carrinhoProdutoRepository,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        INotificador notificador,
        IProdutoService service, IHttpService httpService) : base(notificador)
        {
            _compraRepository = compraRepository;
            _produtoRepository = produtoRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _favoritoRepository = favoritoRepository;
            _carrinhoProdutoRepository = carrinhoProdutoRepository;
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _httpService = httpService;
        }
        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index(string tags, string tipoAnimal, string raca, string porte)
        {
            var selectedTags = new List<string>();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var perfilPet = await _httpService.SendGetAsync<PerfilPet>($"getPerfilUsuario/{user.Id}");

                if (perfilPet != null)
                {
                    ViewBag.NomePet = perfilPet.Nome;
                    ViewBag.TipoAnimal = perfilPet.TipoAnimal;
                    ViewBag.Raca = perfilPet.Raca;
                    ViewBag.Porte = perfilPet.Porte;
                }

            }
            if (tipoAnimal == "on")
            {
                selectedTags.Add(ViewBag.TipoAnimal);
            }
            if (raca == "on")
            {
                selectedTags.Add(ViewBag.Raca);
            }
            if (porte == "on")
            {
                selectedTags.Add(ViewBag.Porte);
            }
            if (!string.IsNullOrEmpty(tags))
            {
                string cleanedTags = tags.Trim().ToUpper();
                string[] tagArray = cleanedTags.Split(',');
                selectedTags.AddRange(tagArray);
            }
            if (selectedTags.Count > 0)
            {
                List<Produto> produtos = null;

                //produtos = await _httpService.SendPostAsync<List<Produto>>("getProdutosTags", selectedTags.ToArray());
                var x = await _service.GetProdutosByTags(selectedTags.ToArray());

                return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(x));
            }
            if (user != null)
            {
                var perfilPet = await _httpService.SendGetAsync<PerfilPet>($"getPerfilUsuario/{user.Id}");

                if (perfilPet != null)
                {
                    ViewBag.NomePet = perfilPet.Nome;
                    ViewBag.TipoAnimal = perfilPet.TipoAnimal;
                    ViewBag.Raca = perfilPet.Raca;
                    ViewBag.Porte = perfilPet.Porte;
                }
            }

            //var produto = await _httpService.SendGetAsync<List<Produto>>("getProdutosAvaliacao");
            var produtosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosOrdenadosPorAvaliacao());
            foreach (var produto in produtosViewModel)
            {
                //Media Avaliação
                double Ava = 0;
                try { Ava = await _avaliacaoRepository.ObterMediaAvaliacaoProduto(produto.Id); } catch { }
                produto.MediaAva = Ava;

            }

            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(produtosViewModel));
        }

        [AllowAnonymous]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {

                produtoViewModel = new ProdutoViewModel
                {
                    Null = true,
                };


                return View(produtoViewModel);
            }


            var produtoHashtags = await _httpService.SendGetAsync<List<ProdutoHashtag>>($"getTagsProduto/{produtoViewModel.Id}");
            produtoViewModel.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);
            produtoViewModel.HashtagsInput = string.Join(", ", produtoViewModel.Hashtags);

            var user = _userManager.FindByIdAsync(produtoViewModel.EstabelecimentoId.ToString());
            ViewBag.NomeDoUsuario = user;
            double Ava = 0;
            try { Ava = await _avaliacaoRepository.ObterMediaAvaliacaoProduto(produtoViewModel.Id); } catch { }
            //Media de Avaliação
            produtoViewModel.MediaAva = Ava;
            //Vendas do Produto no ultimo mes

            produtoViewModel.ComprasUltomoMes = 0;

            var compras  = await _compraRepository.ObterComprasUltimos30Dias();

            foreach (var compra in compras)
            {
                // Verifica se a ListaProduto da compra contém o ID do produto
                if (compra.ListaProdutos.Contains(produtoViewModel.Id.ToString()))
                {
                    produtoViewModel.ComprasUltomoMes++;
                }
            }

            produtoViewModel.Null = false;
            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Usuario", "1")]
        [HttpGet("Produto/favorito")]
        public async Task<IActionResult> Favorito(Guid produtoId)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));

            var favorito = await _favoritoRepository.ObterFavoritosPorUsuario(usuarioId);

            List<FavoritoViewModel> produtosFavViewModel = _mapper.Map<List<FavoritoViewModel>>(favorito);

            foreach (var produtoFav in produtosFavViewModel)
            {
                if (produtoFav.ProdutoId == produtoId)
                {
                    return RedirectToAction("Index");
                }

            }

            var fav = new Favorito()
            {
                ProdutoId = produtoId,
                UsuarioId = usuarioId,
            };
            await _favoritoRepository.Adicionar(fav);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            Console.WriteLine("Entrando no método Create");

            var hashtagStrings = !string.IsNullOrEmpty(produtoViewModel.HashtagsInput) ? produtoViewModel.HashtagsInput.Split(',').Select(tag => tag.Trim()) : null;
            produtoViewModel.Hashtags = hashtagStrings != null ? hashtagStrings.Select(tag => new ProdutoHashtagViewModel { Tag = tag }).ToList() : null;

            Console.WriteLine("Hashtags processadas");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState não é válido");
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = "IMAGEM";
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
            {

                Console.WriteLine("Falha no upload do arquivo");
                return View(produtoViewModel);
            }
            Console.WriteLine("Arquivo enviado com sucesso");

            produtoViewModel.Imagem = produtoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(produtoViewModel.ImagemUpload);
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                produtoViewModel.EstabelecimentoId = Guid.Parse(user.Id);
                produtoViewModel.Estabelecimento = user.Nome;
            }
            else
            {
                Console.WriteLine("Usuário é nulo");
                return View(produtoViewModel);
            }
            Console.WriteLine("Usuário identificado");

            var x = _mapper.Map<Produto>(produtoViewModel);

            try
            {
                Console.WriteLine("Adicionando produto...");
                await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));
                Console.WriteLine("Produto adicionado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar produto:");
                Console.WriteLine(ex.ToString());
            }

            try
            {
                var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + produtoViewModel.ImagemUpload);

                if (System.IO.File.Exists(caminhoCompleto))
                {
                    System.IO.File.Delete(caminhoCompleto);
                    Console.WriteLine("Arquivo excluído com sucesso");
                }
                else
                {
                    Console.WriteLine("O arquivo não existe");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir o arquivo:");
                Console.WriteLine(ex.ToString());
            }

            if (!OperacaoValida())
            {
                Console.WriteLine("Operação inválida");
                return View(produtoViewModel);
            }

            Console.WriteLine("Redirecionando para Index");
            return RedirectToAction("Index");
        }


        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            var produtoHashtags = await _httpService.SendGetAsync<List<ProdutoHashtag>>($"getTagsProduto/{produtoViewModel.Id}");
            produtoViewModel.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);

            double Ava = 0;
            try { Ava = await _avaliacaoRepository.ObterMediaAvaliacaoProduto(produtoViewModel.Id); } catch { }
            produtoViewModel.MediaAva = Ava;

            produtoViewModel.HashtagsInput = string.Join(", ", produtoViewModel.Hashtags.Select(h => h.Tag));


            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);

            var hashtagStrings = !string.IsNullOrEmpty(produtoViewModel.HashtagsInput) ? produtoViewModel.HashtagsInput.Split(',').Select(tag => tag.Trim()) : null;
            produtoViewModel.Hashtags = hashtagStrings != null ? hashtagStrings.Select(tag => new ProdutoHashtagViewModel { Tag = tag }).ToList() : null;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                produtoAtualizacao.Imagem = "IMAGEM";
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoAtualizacao);
                }

                produtoAtualizacao.Imagem = produtoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(produtoViewModel.ImagemUpload);
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;
            produtoAtualizacao.Hashtags = produtoViewModel.Hashtags;


            await _httpService.SendDeleteAsync<List<ProdutoHashtag>>($"excluirPorProduto/{id}");

            try
            {
                await _httpService.SendPutAsync<Produto>("atualizarProduto", _mapper.Map<Produto>(produtoAtualizacao));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }


        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            var produtoHashtags = await _httpService.SendGetAsync<List<ProdutoHashtag>>($"getTagsProduto/{produtoViewModel.Id}");
            produtoViewModel.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);


            produtoViewModel.HashtagsInput = string.Join(", ", produtoViewModel.Hashtags);


            if (produtoViewModel == null)
            {
                return NotFound();
            }

            double Ava = 0;
            try { Ava = await _avaliacaoRepository.ObterMediaAvaliacaoProduto(produtoViewModel.Id); } catch { }
            produtoViewModel.MediaAva = Ava;

            return View(produtoViewModel);
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);
            if (produto == null)
            {
                return NotFound();
            }

            var produtoHashtags = await _httpService.SendGetAsync<List<ProdutoHashtag>>($"getTagsProduto/{produto.Id}");
            produto.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);

            try {await _carrinhoProdutoRepository.RemoverProdutosDoCarrinho(produto.Id);}catch { }
            try{ await _favoritoRepository.RemoverFavoritoPorUsuario(Guid.Parse(_userManager.GetUserId(User)), produto.Id); }catch { }
            try{ await _avaliacaoRepository.ExcluirAvaliacaoPorProdutoId(produto.Id);}catch { }


            //Exclui Produto
            if (produto.Hashtags.Count != 0) await _httpService.SendDeleteAsync<List<ProdutoHashtag>>($"excluirPorProduto/{id}");
            await _httpService.SendDeleteAsync<Produto>($"removerProduto/{id}");



            if (!OperacaoValida()) return View(produto);

            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _httpService.SendGetAsync<Produto>($"getProdutos/{id}"));
            return produto;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        public string ConvertImagemToBase64(IFormFile imagemFile)
        {
            if (imagemFile == null || imagemFile.Length == 0)
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                imagemFile.CopyTo(ms);
                byte[] imagemBytes = ms.ToArray();
                string imagemBase64 = Convert.ToBase64String(imagemBytes);
                return imagemBase64;
            }
        }


    }
}
