using Ipet.Data.Context;
using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Notificacoes;
using Ipet.MVC.Extensions;
using Ipet.MVC.Interfaces;
using Ipet.MVC.Services;
using Ipet.Service.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Ipet.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IHttpService, HttpService>();

            services.AddScoped<ICarrinhoService, CarrinhoService>();
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<ICarrinhoProdutoRepository, CarrinhoProdutoRepository>();

            services.AddScoped<IPerfilPetRepository, PerfilPetRepository>();

            services.AddScoped<IProdutoHashtagRepository, ProdutoHashtagRepository>();

            services.AddScoped<IFavoritoRepository, FavoritoRepository>();

            services.AddScoped<ICompraRepository, CompraRepository>();

            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();

            services.AddScoped<IProdutoHashtagRepository, ProdutoHashtagRepository>();

            services.AddScoped<IHttpService, HttpService>();


            return services;
        }
    }
}