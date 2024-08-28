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
using Ipet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Ipet.MVC.Interfaces;
using Ipet.MVC.Extensions;
using Ipet.Data.Repository;
using Ipet.Domain.Notificacoes;
using Ipet.Domain.Models;

namespace Ipet.MVC.Controllers
{
    public class CompraController : Controller
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IHttpService _httpService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CompraController(IHttpService httpService, ICompraRepository compraRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _compraRepository = compraRepository;
            _userManager = userManager;
            _mapper = mapper;
            _httpService = httpService;
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("compra")]
        public async Task<IActionResult> Index()
        {
            bool confirma = TempData["Pagou"] as bool? ?? false;
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var compra = await _compraRepository.ObterCompraEmAbertoPorUsuario(usuarioId);
            if (compra!= null)
            {
                if(compra.Status== "Pagamento Realizado") confirma = true;
                var compraViewModel = _mapper.Map<CompraViewModel>(compra);

                if (confirma == true) 
                {
                    compraViewModel.Pagou = true;
                    return View(compraViewModel);
                }
                else { compraViewModel.Pagou = false; }

                return View(compraViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Historico");
            }
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("pagar")]
        public async Task<IActionResult> Pagar()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var compra = await _compraRepository.ObterCompraEmAbertoPorUsuario(usuarioId);
            compra.DataCompra = DateTime.Now;
            compra.Status = "Pagamento Realizado";

            await _compraRepository.Atualizar(compra);


            TempData["Pagou"] = true;
            return RedirectToAction("Index", "Compra");
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("pagou")]
        public async Task<IActionResult> pagou()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var compra = await _compraRepository.ObterCompraEmAbertoPorUsuario(usuarioId);
            compra.Status = "Aprovado";

            await _compraRepository.Atualizar(compra);


            TempData["Pagou"] = false;
            return RedirectToAction("Index", "historico");
        }
        [ClaimsAuthorize("Usuario", "1")]
        [Route("cancelar")]
        public async Task<IActionResult> Cancelar()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var compra = await _compraRepository.ObterCompraEmAbertoPorUsuario(usuarioId);
            compra.Status = "Cancelado";
            compra.DataCompra = DateTime.Now;

            await _compraRepository.Atualizar(compra);


            TempData["Pagou"] = false;
            return RedirectToAction("Index", "historico");
        }


        [Route("historico")]
        public async Task<IActionResult> HistoricoCompra()
        {
            TempData["Pagou"] = false;
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var compra = await _compraRepository.ObterComprasPorUsuario(usuarioId);
            if (compra != null)
            {
                return View(_mapper.Map<List<CompraViewModel>>(compra));
            }

            return View();
        }

    }
}
