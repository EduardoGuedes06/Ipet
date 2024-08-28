namespace Ipet.Domain.Models
{
    public class PerfilPet : Entity
    {
        public Guid IdUsuario { get; set; }
        public string TipoAnimal { get; set; }

        public string Nome { get; set; }

        public string Raca { get; set; }

        public int Idade { get; set; }

        public string Porte { get; set; }

        public string Observacao { get; set; }
        public bool Ativo { get; set; } 
    }

}
