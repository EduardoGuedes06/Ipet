using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.MinimalApi.Requests
{
    public class PerfilPetRequest
    {
        public Guid IdUsuario { get; set; }
        public string TipoAnimal { get; set; }

        public string Nome { get; set; }

        public string Raca { get; set; }

        public int Idade { get; set; }

        public string Porte { get; set; }

        public string Observacao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
