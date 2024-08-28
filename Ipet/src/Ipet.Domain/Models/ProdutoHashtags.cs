using Ipet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class ProdutoHashtag : Entity
    {
        public Guid IdProduto { get; set; }
        public string Tag { get; set; }
        public Produto Produto { get; set; }
    }
}
