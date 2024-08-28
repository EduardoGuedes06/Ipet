using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ipet.MVC.Data;
using Ipet.ViewModels;
using AutoMapper;
using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.MVC.Interfaces;
using Ipet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Ipet.MVC.Extensions;
using Ipet.Domain.Models;

namespace Ipet.MVC.Controllers
{
    public class AvaliacaoController : BaseController
    {
        private readonly IHttpService _httpService;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IProdutoRepository _produtoRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public AvaliacaoController(IAvaliacaoRepository avaliacaoRepository,
                                   IProdutoRepository produtoRepo,
                                   IHttpService httpService, 
                                   INotificador notificador, 
                                   IMapper mapper, UserManager<ApplicationUser> userManager) : base(notificador)
        {
            _produtoRepo = produtoRepo;
            _avaliacaoRepository = avaliacaoRepository;
            _userManager = userManager;
            _mapper = mapper;
            _httpService = httpService;
        }


        // GET: Avaliacao
        //public async Task<IActionResult> Index()
        //{
        //    var ipetMVCContext2222 = _context.AvaliacaoViewModel.Include(a => a.Produto);
        //    return View(await ipetMVCContext2222.ToListAsync());
        //}

        [ClaimsAuthorize("Usuario", "1")]
        [Route("avaliar/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _httpService.SendGetAsync<Produto>($"getProdutos/{id}"));
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            
            if (produto == null)
            {
                var avaliacao = new AvaliacaoViewModel
                {
                    UserId = usuarioId,
                    Null = true,
                };
                return View(avaliacao);
            }

            var avaliacaoViewModel = _mapper.Map<AvaliacaoViewModel>(await _avaliacaoRepository.ObterAvaliacaoProdutoUsuario(produto.Id, usuarioId));



            if (avaliacaoViewModel == null)
            {
                avaliacaoViewModel = new AvaliacaoViewModel
                {
                    UserId = usuarioId,
                    ProdutoId = produto.Id,
                    Produto = produto,
                    Estrelas = 0,
                };
            }
            avaliacaoViewModel.Produto= produto;
            return View(avaliacaoViewModel);
        }

        [Route("Avaliar")]
        [HttpPost]
        public async Task<IActionResult> Avaliar(AvaliacaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = Guid.Parse(_userManager.GetUserId(User));

                var antigaAvaliacao = _mapper.Map<AvaliacaoViewModel>(await _avaliacaoRepository.ObterAvaliacaoProdutoUsuario(model.ProdutoId, usuarioId));

                if (antigaAvaliacao == null) 
                {
                    Avaliacao avaliacao = new Avaliacao
                    {
                        UserId = usuarioId,
                        ProdutoId = model.ProdutoId,
                        Estrelas = model.Estrelas,
                    };

                    await _avaliacaoRepository.Adicionar(avaliacao);

                }
                else 
                {
                    antigaAvaliacao.Estrelas = model.Estrelas;
                    await _avaliacaoRepository.Atualizar(_mapper.Map<Avaliacao>(antigaAvaliacao));
                }




                return RedirectToAction("Index", "Produtos");
            }

            return View(model);
        }

    }
}
