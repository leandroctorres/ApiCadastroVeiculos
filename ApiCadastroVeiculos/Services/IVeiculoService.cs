using ApiCadastroVeiculos.InputModel;
using ApiCadastroVeiculos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Services
{
    public interface IVeiculoService : IDisposable
    {
        Task<List<VeiculoViewModel>> Obter(int pagina, int quantidade);

        Task<VeiculoViewModel> Obter(Guid id);

        Task<VeiculoViewModel> Inserir(VeiculoInputModel jogo);

        Task Atualizar(Guid id, VeiculoInputModel jogo);
        Task Atualizar(Guid id, double preco);

        Task Remover(Guid id);
    }
}