using ApiCadastroVeiculos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Repositories
{
    public interface IVeiculoRepository : IDisposable
    {
        Task<List<Veiculo>> Obter(int pagina, int quantidade);
        Task<Veiculo> Obter(Guid id);
        Task<List<Veiculo>> Obter(string nome, string produtora);
        Task Inserir(Veiculo veiculo);
        Task Atualizar(Veiculo veiculo);
        Task Remover(Guid id);
    }
}
