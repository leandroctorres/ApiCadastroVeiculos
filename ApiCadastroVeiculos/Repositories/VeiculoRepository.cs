using ApiCadastroVeiculos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private static Dictionary<Guid, Veiculo> veiculos = new Dictionary<Guid, Veiculo>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd84"), new Veiculo{ Id = Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd84"), Marca = "Renault", Modelo = "Kwid", Ano = 2017 , Preco = 48790 } },
            {Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd85"), new Veiculo{ Id = Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd85"), Marca = "Fiat", Modelo = "Mobi", Ano = 2016 , Preco = 49240 } },
            {Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd86"), new Veiculo{ Id = Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd86"), Marca = "Fiat", Modelo = "Uno", Ano = 2017 , Preco = 64990 } },
            {Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd87"), new Veiculo{ Id = Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd87"), Marca = "Hyundai", Modelo = "HB20", Ano = 2018 , Preco = 64190 } },
            {Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd88"), new Veiculo{ Id = Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd88"), Marca = "Chevrolet", Modelo = "Joy", Ano = 2017 , Preco = 65240 } },
            {Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd89"), new Veiculo{ Id = Guid.Parse("0ca314a5-9282-45d8-93c3-2985f2a9fd89"), Marca = "Volkswagen", Modelo = "Gol", Ano = 2020 , Preco =  65590} }
        };

        public Task<List<Veiculo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(veiculos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Veiculo> Obter(Guid id)
        {
            if (!veiculos.ContainsKey(id))
                return null;

            return Task.FromResult(veiculos[id]);
        }

        public Task<List<Veiculo>> Obter(string marca, string modelo)
        {
            return Task.FromResult(veiculos.Values.Where(veiculo => veiculo.Marca.Equals(marca) && veiculo.Modelo.Equals(modelo)).ToList());
        }

        public Task<List<Veiculo>> ObterSemLambda(string marca, string modelo)
        {
            var retorno = new List<Veiculo>();

            foreach (var veiculo in veiculos.Values)
            {
                if (veiculo.Marca.Equals(marca) && veiculo.Modelo.Equals(modelo))
                    retorno.Add(veiculo);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Veiculo veiculo)
        {
            veiculos.Add(veiculo.Id, veiculo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Veiculo veiculo)
        {
            veiculos[veiculo.Id] = veiculo;
            return Task.CompletedTask;
        }


        public Task Remover(Guid id)
        {
            veiculos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexãp com o banco.
        }

    }
}
