
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
    public class PerfilPetController : BaseController
    {
        private readonly IPerfilPetRepository _repo;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PerfilPetController(IPerfilPetRepository perfilPetRepository,
        IMapper mapper,UserManager<ApplicationUser> userManager,
        INotificador notificador, IHttpService httpService, IPerfilPetRepository repo) : base(notificador)
        {
            _repo = perfilPetRepository;
            _httpService = httpService;
            _mapper = mapper;
            _userManager = userManager;
            _repo = repo;
        }

        [AllowAnonymous]
        [Route("perfil")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            //var x = await _perfilPetRepository.ObterPerfilUsuario((Guid.Parse(user.Id)));

            //var x = ObterPerfilPet(user.Id);

            var x = await _httpService.SendGetAsync<PerfilPet>($"getPerfilUsuario/{user.Id}");

            return View(_mapper.Map<PerfilPetViewModel>(x));
        }


        [AllowAnonymous]
        [Route("perfil/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var perfilPetViewModel = await ObterPerfilPetByUser(id.ToString());

            if (perfilPetViewModel == null)
            {
                return NotFound();
            }
            return View(perfilPetViewModel);
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("novo-perfil")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("novo-perfil")]
        [HttpPost]
        public async Task<IActionResult> Create(PerfilPetViewModel perfilPetViewModel)
        {


            var user = await _userManager.GetUserAsync(User);

            var perfilAtual = await ObterPerfilPetByUser(user.Id);

            perfilPetViewModel.IdUsuario = Guid.Parse(user.Id);
            perfilPetViewModel.Id = Guid.Parse(user.Id);
            perfilPetViewModel.Ativo = true;
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        var errorMessage = error.ErrorMessage;

                    }
                }
                return View(perfilPetViewModel);
            }

            if (user != null)
            {
                perfilPetViewModel.IdUsuario = Guid.Parse(user.Id);
            }
            else
            {
                return View(perfilPetViewModel);
            }
            if (perfilAtual != null)
            {
                return View(perfilPetViewModel);
            }
            else
            {

                //await _perfilPetService.Adicionar(_mapper.Map<PerfilPet>(perfilPetViewModel));
                await _httpService.SendPostAsync<PerfilPet>($"adicionarPerfilPet", _mapper.Map<PerfilPet>(perfilPetViewModel));

                if (!OperacaoValida()) return View(perfilPetViewModel);
            }
            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("editar-perfil/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var perfilPetViewModel = await ObterPerfilPetById(id.ToString());


            if (perfilPetViewModel == null)
            {
                return NotFound();
            }

            return View(perfilPetViewModel);
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("editar-perfil/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PerfilPetViewModel perfilPetViewModel)
        {
            if (id != perfilPetViewModel.Id) return NotFound();

            var perfilAtualizacao = await ObterPerfilPetById(id.ToString());

            if (!ModelState.IsValid) return View(perfilPetViewModel);

            perfilAtualizacao.Nome = perfilPetViewModel.Nome;
            perfilAtualizacao.Porte = perfilPetViewModel.Porte;
            perfilAtualizacao.Idade = perfilPetViewModel.Idade;
            perfilAtualizacao.TipoAnimal = perfilPetViewModel.TipoAnimal;
            perfilAtualizacao.Raca = perfilPetViewModel.Raca;
            perfilAtualizacao.Ativo = perfilPetViewModel.Ativo;
            perfilAtualizacao.Observacao = perfilPetViewModel.Observacao;

            await _repo.Atualizar(_mapper.Map<PerfilPet>(perfilAtualizacao));

            //await _httpService.SendPutAsync<PerfilPet>("atualizarPerfilPet", _mapper.Map<PerfilPet>(perfilAtualizacao));
            //await _httpService.SendPostAsync<PerfilPet>($"adicionarPerfilPet", _mapper.Map<PerfilPet>(perfilPetViewModel));
            //await _httpService.SendPutAsync<PerfilPet>($"atualizarPerfilPet", _mapper.Map<PerfilPet>(perfilAtualizacao));

            if (!OperacaoValida()) return View(perfilPetViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Usuario", "1")]
        [Route("excluir-perfil/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterPerfilPetById(id.ToString());

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        [ClaimsAuthorize("Usuario", "1")]
        [Route("excluir-perfil/{id:guid}")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var perfil = await ObterPerfilPetById(id.ToString());

            if (perfil == null)
            {
                return NotFound();
            }

            //await _perfilPetService.Remover(id);
            await _httpService.SendDeleteAsync<PerfilPet>($"removerPerfilPet/{id}");

            if (!OperacaoValida()) return View(perfil);

            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }
        private async Task<PerfilPetViewModel> ObterPerfilPetByUser(string id)
        {
            //var perfil = _mapper.Map<PerfilPetViewModel>(await _perfilPetRepository.ObterPorId(id));
            var perfil = _mapper.Map<PerfilPetViewModel>(await _httpService.SendGetAsync<PerfilPet>($"getPerfilUsuario/{id}"));
            return perfil;
        }

        private async Task<PerfilPetViewModel> ObterPerfilPetById(string id)
        {
            //var perfil = _mapper.Map<PerfilPetViewModel>(await _perfilPetRepository.ObterPorId(id));
            var perfil = _mapper.Map<PerfilPetViewModel>(await _httpService.SendGetAsync<PerfilPet>($"getPerfilPet/{id}"));
            return perfil;
        }

    }
}
