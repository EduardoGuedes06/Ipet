using Ipet.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class ServiçoHashtagViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdServico { get; set; }
        public string Tag { get; set; }
    }
}
