using Ipet.MVC.Models;

namespace Ipet.MVC.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
        Task<UsuarioRespostaLogin> RegistroEstabelecimento(UsuarioEstabelecimentoRegistro usuarioRegistro);
    }
}
