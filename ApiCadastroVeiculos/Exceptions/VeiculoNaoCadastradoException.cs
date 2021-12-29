using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Exceptions
{
    public class VeiculoNaoCadastradoException : Exception
    {
        public VeiculoNaoCadastradoException()
            : base("Este veículo não está cadastrado")
        { }
    }
}
