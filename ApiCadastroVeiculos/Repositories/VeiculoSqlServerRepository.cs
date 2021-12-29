using ApiCadastroVeiculos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Repositories
{
    public class VeiculoSqlServerRepository : IVeiculoRepository
    {
        private readonly SqlConnection sqlConnection;

        public VeiculoSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Veiculo>> Obter(int pagina, int quantidade)
        {
            var veiculos = new List<Veiculo>();

            var comando = $"select * from Veiculos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                veiculos.Add(new Veiculo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Marca = (string)sqlDataReader["Marca"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Ano = (int)sqlDataReader["Ano"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return veiculos;
        }

        public async Task<Veiculo> Obter(Guid id)
        {
            Veiculo veiculo = null;

            var comando = $"select * from Veiculos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                veiculo = new Veiculo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Marca = (string)sqlDataReader["Marca"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Ano = (int)sqlDataReader["Ano"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return veiculo;
        }

        public async Task<List<Veiculo>> Obter(string marca, string modelo)
        {
            var veiculos
                = new List<Veiculo>();

            var comando = $"select * from veiculos where Marca = '{marca}' and Modelo = '{modelo}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                veiculos.Add(new Veiculo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Marca = (string)sqlDataReader["Marca"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Ano = (int)sqlDataReader["Ano"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return veiculos;
        }

        public async Task Inserir(Veiculo veiculo)
        {
            var comando = $"insert Veiculos (Id, Marca, Modelo, Ano, Preco) values ('{veiculo.Id}', '{veiculo.Marca}', '{veiculo.Modelo}', {veiculo.Ano}, {veiculo.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            //sqlCommand.ExecuteNonQuery();
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            var comando = $"update Veiculos set Marca = '{veiculo.Marca}', Modelo = '{veiculo.Modelo}', Ano = {veiculo.Ano}, Preco = {veiculo.Preco.ToString().Replace(",", ".")} where Id = '{veiculo.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Veiculos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

    }
}
