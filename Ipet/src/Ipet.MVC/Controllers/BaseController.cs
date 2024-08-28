using Ipet.Domain.Intefaces;
using Ipet.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ipet.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly INotificador _notificador;

        internal BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        internal bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        internal bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}