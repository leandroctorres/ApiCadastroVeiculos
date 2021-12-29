using ApiCadastroVeiculos.Entities;
using ApiCadastroVeiculos.Exceptions;
using ApiCadastroVeiculos.InputModel;
using ApiCadastroVeiculos.Repositories;
using ApiCadastroVeiculos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoService(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public async Task<List<VeiculoViewModel>> Obter(int pagina, int quantidade)
        {
            var veiculos = await _veiculoRepository.Obter(pagina, quantidade);

            return veiculos.Select(veiculo => new VeiculoViewModel
            {
                Id = veiculo.Id,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Preco = veiculo.Preco
            })
                .ToList();
        }

        public async Task<VeiculoViewModel> Obter(Guid id)
        {
            var veiculo = await _veiculoRepository.Obter(id);

            if (veiculo == null)
                return null;

            return new VeiculoViewModel
            {
                Id = veiculo.Id,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Preco = veiculo.Preco
            };
        }

        public async Task<VeiculoViewModel> Inserir(VeiculoInputModel veiculo)
        {
            var entidadeVeiculo = await _veiculoRepository.Obter(veiculo.Marca, veiculo.Modelo);

            if (entidadeVeiculo.Count > 0)
                throw new VeiculoJaCadastradoException();

            var veiculoInsert = new Veiculo
            {
                Id = Guid.NewGuid(),
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Preco = veiculo.Preco
            };

            await _veiculoRepository.Inserir(veiculoInsert);

            return new VeiculoViewModel
            {
                Id = veiculoInsert.Id,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Preco = veiculo.Preco
            };

        }

        public async Task Atualizar(Guid id, VeiculoInputModel veiculo)
        {
            var entidadeVeiculo = await _veiculoRepository.Obter(id);

            if (entidadeVeiculo == null)
                throw new VeiculoNaoCadastradoException();

            entidadeVeiculo.Marca = veiculo.Marca;
            entidadeVeiculo.Modelo = veiculo.Modelo;
            entidadeVeiculo.Ano = veiculo.Ano;
            entidadeVeiculo.Preco = veiculo.Preco;

            await _veiculoRepository.Atualizar(entidadeVeiculo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeVeiculo = await _veiculoRepository.Obter(id);

            if (entidadeVeiculo == null)
                throw new VeiculoNaoCadastradoException();

            entidadeVeiculo.Preco = preco;

            await _veiculoRepository.Atualizar(entidadeVeiculo);
        }

        public async Task Remover(Guid id)
        {
            var veiculo = await _veiculoRepository.Obter(id);

            if (veiculo == null)
                throw new VeiculoNaoCadastradoException();

            await _veiculoRepository.Remover(id);
        }

        public void Dispose()
        {
            _veiculoRepository.Dispose();
        }
    }
}
