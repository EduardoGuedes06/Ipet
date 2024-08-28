using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Ipet.MVC.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataCPF(this RazorPage page,string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return cpf; // Retornar o valor original se não for válido ou já estiver formatado

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string FormataCNPJ(this RazorPage page,string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || cnpj.Length != 14)
                return cnpj; // Retornar o valor original se não for válido ou já estiver formatado

            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
        public static string MarcarOpcao(this RazorPage page, int tipoPessoa, int valor)
        {
            return tipoPessoa == valor ? "checked" : "";
        }

        //public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        //{
        //    return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);
        //}

        //public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
        //{
        //    return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue) ? "" : "disabled";
        //}

        //public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string claimValue)
        //{
        //    return CustomAuthorization.ValidarClaimsUsuario(context, claimName, claimValue) ? page : null;
        //}
    }
}