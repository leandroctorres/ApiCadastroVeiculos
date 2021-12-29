using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.ViewModel
{
    public class VeiculoViewModel
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Preco { get; set; }
    }
}
