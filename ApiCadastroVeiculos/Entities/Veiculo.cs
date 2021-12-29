using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Entities
{
    public class Veiculo
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Preco { get; set; }
    }
}
