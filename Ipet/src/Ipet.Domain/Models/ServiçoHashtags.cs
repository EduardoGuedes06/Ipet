using Ipet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class ServiçoHashtag : Entity
    {
        public Guid IdServico { get; set; }
        public string Tag { get; set; }

        public Servico servico { get; set; }
    }
}
