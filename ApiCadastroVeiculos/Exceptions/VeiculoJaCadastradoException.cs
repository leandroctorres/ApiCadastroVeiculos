using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Exceptions
{
    public class VeiculoJaCadastradoException : Exception
    {
        public VeiculoJaCadastradoException()
            : base("Este veículo já está cadastrado")
        { }
    }
}
