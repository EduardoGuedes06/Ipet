using Ipet.Data.Context;
using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Ipet.Domain.Notificacoes;
using Ipet.MinimalApi.Requests;
using Ipet.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Context

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseMySql("server=g-tech-dev-web.duckdns.org;initial catalog = Ipet;uid=Admin;pwd=MyAdmin@2024",
//options.UseMySql("server=localhost;initial catalog = ipet;uid=root;pwd=root",
Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#endregion

#region Register Services 

builder.Services.AddScoped<MeuDbContext>();
builder.Services.AddScoped<INotificador, Notificador>();

builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<ICarrinhoProdutoRepository, CarrinhoProdutoRepository>();

builder.Services.AddScoped<IPerfilPetRepository, PerfilPetRepository>();


builder.Services.AddScoped<IProdutoHashtagRepository, ProdutoHashtagRepository>();

builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();

builder.Services.AddScoped<ICompraRepository, CompraRepository>();

builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();

#endregion

var app = builder.Build();

#region Produto 

// Obter Todos 
app.MapGet("/getProdutos", async (IProdutoRepository repo) =>
    await repo.ObterTodos());

// Obter por Avaliação
app.MapGet("/getProdutosAvaliacao", async (IProdutoRepository repo) =>
    await repo.ObterProdutosOrdenadosPorAvaliacao());

// Obter por Id 
app.MapGet("/getProdutos/{id}", async (Guid id, IProdutoRepository repo) =>
    await repo.ObterPorId(id));

// Obter por Tags 
app.MapPost("/getProdutosTags", async (string[] tags, IProdutoService service) =>
    await service.GetProdutosByTags(tags));

// Adicionar 
app.MapPost("/adicionarProduto", async (Produto produto, IProdutoRepository repo) =>
    await repo.Adicionar(new Produto()
    {
        Ativo = true,
        Descricao = produto.Descricao,
        EstabelecimentoId = produto.EstabelecimentoId,
        Imagem = produto.Imagem,
        Estabelecimento = produto.Estabelecimento,
        Nome = produto.Nome,
        Valor = produto.Valor,
        Hashtags = produto.Hashtags,
    }));

// Atualizar
// Descontinuado
app.MapPut("/atualizarProduto", async (Produto produto, IProdutoRepository repo) =>
    await repo.Atualizar(new Produto()
    {
        Id = produto.Id,
        Ativo = true,
        Descricao = produto.Descricao,
        EstabelecimentoId = produto.EstabelecimentoId,
        Imagem = produto.Imagem,
        Estabelecimento = produto.Estabelecimento,
        Nome = produto.Nome,
        Valor = produto.Valor,
        Hashtags = produto.Hashtags,
    }));

// Remover 
app.MapDelete("/removerProduto/{id}", async (Guid id, IProdutoRepository repo) =>
    await repo.Remover(id));



#endregion

#region Perfil Pet 

// Obter Todos 
app.MapGet("/getPerfis", async (IPerfilPetRepository repo) =>
    await repo.ObterTodos());

// Obter por Id 
app.MapGet("/getPerfilPet/{id}", async (Guid id, IPerfilPetRepository repo) =>
    await repo.ObterPorId(id));

// Obter por Usuario 
app.MapGet("/getPerfilUsuario/{userId}", async (Guid userId, IPerfilPetRepository repo) =>
    await repo.ObterPerfilUsuario(userId));

// Adicionar 
app.MapPost("/adicionarPerfilPet", async (PerfilPetRequest pet, IPerfilPetRepository repo) =>
    await repo.Adicionar(new PerfilPet()
    {
        Ativo = true,
        Idade = pet.Idade,
        IdUsuario = pet.IdUsuario,
        Raca = pet.Raca,
        Nome = pet.Nome,
        Observacao = pet.Observacao,
        Porte = pet.Porte,
        TipoAnimal = pet.TipoAnimal
    }));

// Atualizar  
// Descontinuado
app.MapPut("/atualizarPerfilPet", async (PerfilPet pet, IPerfilPetRepository repo) =>
    await repo.Adicionar(pet));

// Remover 
app.MapDelete("/removerPerfilPet/{id}", async (Guid id, IPerfilPetRepository repo) =>
    await repo.Remover(id));

#endregion

#region Avaliação 

// Obter Todos 
app.MapGet("/getAvaliacoes", async (IAvaliacaoRepository repo) =>
    await repo.ObterTodos());

// Obter Media
app.MapGet("/getMediaAvaliacao{produtoId}", async (Guid produtoId, IAvaliacaoRepository repo) =>
    await repo.ObterMediaAvaliacaoProduto(produtoId));

// Obter por Id 
app.MapGet("/getAvaliacoes/{id}", async (Guid id, IAvaliacaoRepository repo) =>
    await repo.ObterPorId(id));

// Obter por Produto
app.MapGet("/getAvaliacoesProduto/{produtoId}", async (Guid produtoId, IAvaliacaoRepository service) =>
    await service.ObterAvaliacoesPorProduto(produtoId));

// Adicionar 
app.MapPost("/adicionarAvaliacao", async (AdicionarAvaliacaoRequest avaliacao, IAvaliacaoRepository repo) =>
    await repo.Adicionar(new Avaliacao()
    {
        ProdutoId = avaliacao.ProdutoId,
        Estrelas = avaliacao.Estrelas
    }));

// Atualizar  
app.MapPut("/atualizarAvaliacao", async (AtualizarAvaliacaoRequest avaliacao, IAvaliacaoRepository repo) =>
    await repo.Adicionar(new Avaliacao()
    {
        Id = avaliacao.Id,
        Estrelas = avaliacao.Estrelas,
        ProdutoId = avaliacao.ProdutoId
    }));

// Remover 
app.MapDelete("/removerAvaliacao/{id}", async (Guid id, IAvaliacaoRepository repo) =>
    await repo.Remover(id));

#endregion

#region Produto Hastag

// Obter Todos 
app.MapGet("/getTags", async (IProdutoHashtagRepository repo) =>
    await repo.ObterTodos());

// Obter por Produto
app.MapGet("/getTagsProduto/{produtoId}", async (Guid produtoId, IProdutoHashtagRepository repo) =>
    await repo.ObterPorProdutoId(produtoId));

// Obter por Id 
app.MapGet("/getTags/{id}", async (Guid id, IProdutoHashtagRepository repo) =>
    await repo.ObterPorId(id));

// Excluir por produto 
app.MapDelete("/excluirPorProduto/{produtoId}", async (Guid produtoId, IProdutoHashtagRepository repo) =>
    await repo.ExcluirTagsDoProduto(produtoId));

// Adicionar 
app.MapPost("/adicionarTag", async (ProdutoHashtag tag, IProdutoHashtagRepository repo) =>
    await repo.Adicionar(new ProdutoHashtag()
    {
        IdProduto = tag.IdProduto, 
        Tag = tag.Tag
    }));

// Atualizar  
app.MapPut("/atualizarTag", async (ProdutoHashtag tag, IProdutoHashtagRepository repo) =>
    await repo.Adicionar(new ProdutoHashtag()
    {
        Id = tag.Id,
        IdProduto = tag.IdProduto, 
        Tag = tag.Tag
    }));

// Remover 
app.MapDelete("/removerTag/{id}", async (Guid id, IProdutoHashtagRepository repo) =>
    await repo.Remover(id));
#endregion

#region Carrinho 

// Obter Todos 
app.MapGet("/getCarrinho", async (ICarrinhoRepository repo) =>
    await repo.ObterTodos());

// Obter por Usuario
app.MapGet("/getCarrinhoUsuario/{usuarioId}", async (Guid usuarioId, ICarrinhoRepository repo) =>
    await repo.ObterCarrinhoPorUsuario(usuarioId));

// Obter por Id 
app.MapGet("/getCarrinho/{id}", async (Guid id, ICarrinhoRepository repo) =>
    await repo.ObterPorId(id));

// Obter Usuario
app.MapGet("/getUsuario/{usuarioId}", async (Guid usuarioId, ICarrinhoRepository service) =>
    await service.ObterUsuarioPorId(usuarioId));

// Adicionar 
app.MapPost("/adicionarCarrinho", async (Carrinho carrinho, ICarrinhoRepository repo) =>
    await repo.Adicionar(new Carrinho()
    {
        CarrinhoProdutos = carrinho.CarrinhoProdutos,
        UsuarioId = carrinho.UsuarioId,
    }));

// Atualizar  
app.MapPut("/atualizarCarrinho", async (Carrinho carrinho, ICarrinhoRepository repo) =>
    await repo.Atualizar(new Carrinho()
    {
        Id = carrinho.Id,
        CarrinhoProdutos = carrinho.CarrinhoProdutos,
        UsuarioId = carrinho.UsuarioId
    }));

// Remover 
app.MapDelete("/removerCarrinho/{id}", async (Guid id, ICarrinhoRepository repo) =>
    await repo.Remover(id));

// Adicionar produto ao carrinho 
app.MapGet("/adicionarProdutoCarrinho{userId}/{produtoId}/{quantidade}", async (Guid userId, Guid produtoId, int quantidade, ICarrinhoService repo) =>
    await repo.AdicionarProduto(userId, produtoId, quantidade));

// Adicionar quantidade produto 
app.MapGet("/adicionarQuantidadeProduto{userId}/{produtoId}/{quantidade}", async (Guid userId, Guid produtoId, int quantidade, ICarrinhoService repo) =>
    await repo.AtualizarQuantidadeProduto(userId, produtoId, quantidade));

// Remover produto 
app.MapDelete("/removerProdutoCarrinho{userId}/{produtoId}/{quantidade}", async (Guid userId, Guid produtoId, int quantidade, ICarrinhoService repo) =>
    await repo.RemoverProduto(userId, produtoId));

// Criar carrinho 
app.MapGet("/criarCarrinho{userId}", async (Guid userId, ICarrinhoService repo) =>
    await repo.CriarCarrinho(userId));

// Remover carrinho 
app.MapGet("/removerCarrinho{userId}", async (Guid userId, ICarrinhoService repo) =>
    await repo.RemoverCarrinho(userId));

// Remover ProdutosCarrinho por Produto
app.MapGet("/removerProdutosCarrinho{produtoId}", async (Guid produtoId, ICarrinhoProdutoRepository repo) =>
    await repo.RemoverProdutosDoCarrinho(produtoId));

#endregion

#region Compra 

// Obter por Usuario 
app.MapGet("/getComprasPorUsuario/{usuarioId}", async (Guid usuarioId, ICompraRepository repo) =>
    await repo.ObterComprasPorUsuario(usuarioId));

// Adicionar 
app.MapPost("/adicionarCompra", async (Compra compra, ICompraRepository repo) =>
    await repo.Adicionar(new Compra()
    {
       // CarrinhoProdutos = compra.CarrinhoProdutos,
        UsuarioId = compra.UsuarioId,
        Status = compra.Status
    }));

// Atualizar  
app.MapPut("/atualizarCompra", async (Compra compra, ICompraRepository repo) =>
    await repo.Atualizar(new Compra()
    {
        //CarrinhoProdutos = compra.CarrinhoProdutos,
        UsuarioId = compra.UsuarioId,
        Status = compra.Status
    }));

// Remover 
app.MapDelete("/removerCompra/{id}", async (Guid id, ICompraRepository repo) =>
    await repo.Remover(id));

#endregion

#region Favoritos

// Obter por Usuario 
app.MapGet("/getFavoritosPorUsuario/{usuarioId}", async (Guid usuarioId, IFavoritoRepository repo) =>
    await repo.ObterFavoritosPorUsuario(usuarioId));

// Adicionar 
app.MapPost("/adicionarFavorito", async (Favorito favorito, IFavoritoRepository repo) =>
    await repo.Adicionar(new Favorito()
    {
        ProdutoId = favorito.ProdutoId,
        UsuarioId = favorito.UsuarioId,
    }));

// Remover 
app.MapDelete("/removerFavorito/{usuarioId}/{produtoId}", async (Guid usuarioId, Guid produtoId, IFavoritoRepository repo) =>
    await repo.RemoverFavoritoPorUsuario(usuarioId, produtoId));

#endregion

app.Run();
