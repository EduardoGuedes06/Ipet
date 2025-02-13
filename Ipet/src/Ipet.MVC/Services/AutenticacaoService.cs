﻿using Ipet.Domain.Intefaces;
using Ipet.MVC.Extensions;
using Ipet.MVC.Interfaces;
using Ipet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Ipet.MVC.Services
{
    public class AutenticacaoService : HttpExtension, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;


        public AutenticacaoService(HttpClient httpClient,
                                    UserManager<ApplicationUser> userManager,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("nova-conta", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
        public async Task<UsuarioRespostaLogin> RegistroEstabelecimento(UsuarioEstabelecimentoRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("nova-conta", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}
