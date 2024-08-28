using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ipet.ViewModels;
using AutoMapper;
using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.MVC.Interfaces;
using Ipet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Ipet.Service.Services;
using Ipet.MVC.Extensions;

namespace Ipet.MVC.Controllers
{
    public class FavoritoController : Controller
    {
        private readonly IHttpService _httpService;
        private readonly IProdutoRepository _produtoRepo;
        private readonly ICarrinhoProdutoRepository _carrinhoProdutoRepository;
        private readonly IFavoritoRepository _favoriteRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public FavoritoController(
                                  IProdutoRepository produtoRepo,
                                  IFavoritoRepository favoriteRepository,
                                  ICarrinhoRepository carrinhoRepo, IHttpService httpService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _favoriteRepository = favoriteRepository;
            _produtoRepo = produtoRepo;
            _userManager = userManager;
            _mapper = mapper;
            _httpService = httpService;
        }

        [ClaimsAuthorize("Usuario", "1")]
        [HttpGet("favorito")]
        public async Task<IActionResult> Index()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));

            var fav = await _favoriteRepository.ObterFavoritosPorUsuario(usuarioId);
            List<FavoritoViewModel> produtosFavViewModel = _mapper.Map<List<FavoritoViewModel>>(fav);

            ViewData["Title"] = "Favoritos";
            if (fav != null)
            {
                foreach (var produtoFav in produtosFavViewModel)
                {
                    var produto = await _produtoRepo.ObterPorId(produtoFav.ProdutoId);
                    var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
                    produtoFav.Produto = produtoViewModel;
                }
                return View(produtosFavViewModel);
            }
            else
            {
                return View();
            }
        }

        [ClaimsAuthorize("Usuario", "1")]
        [HttpPost("favorito/remover")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            await _favoriteRepository.RemoverFavoritoPorUsuario(usuarioId, produtoId);
            return RedirectToAction("Index");
        }
    }
}
