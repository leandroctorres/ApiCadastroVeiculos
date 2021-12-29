using ApiCadastroVeiculos.Exceptions;
using ApiCadastroVeiculos.InputModel;
using ApiCadastroVeiculos.Services;
using ApiCadastroVeiculos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculosController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        /// <summary>
        /// Busca todos os veiculos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os veículos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página esta sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de veículos</response>
        /// <response code="204">Caso não haja veículos</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var veiculos = await _veiculoService.Obter(pagina, quantidade);

            if (veiculos.Count() == 0)
                return NoContent();

            return Ok(veiculos);
        }

        /// <summary>
        /// Busca um veículo pelo seu Id
        /// </summary>
        /// <param name="idVeiculo">Id do veículo buscado</param>
        /// <response code="200">Retorna o veículo filtrado</response>
        /// <response code="204">Caso não haja veículo com este id</response>
        [HttpGet("{idVeiculo:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Obter([FromRoute] Guid idVeiculo)
        {
            var veiculo = await _veiculoService.Obter(idVeiculo);

            if (veiculo == null)
                return NoContent();

            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<ActionResult<VeiculoViewModel>> InserirVeiculo([FromBody] VeiculoInputModel veiculoInputModel)
        {
            try
            {
                var veiculo = await _veiculoService.Inserir(veiculoInputModel);

                return Ok(veiculo);
            }
            catch (VeiculoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um veículo com esta marca e modelo");
            }
        }

        [HttpPut("{idVeiculo:guid}")]
        public async Task<ActionResult> AtualizarVeiculo([FromRoute] Guid idVeiculo, [FromBody] VeiculoInputModel veiculoInputModel)
        {
            try
            {
                await _veiculoService.Atualizar(idVeiculo, veiculoInputModel);

                return Ok();
            }
            catch (VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veículo");
            }
        }

        [HttpPatch("{idVeiculo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarVeiculo([FromRoute] Guid idVeiculo, [FromRoute] double preco)
        {
            try
            {
                await _veiculoService.Atualizar(idVeiculo, preco);

                return Ok();
            }
            catch (VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veiculo");
            }
        }

        [HttpDelete("{idVeiculo:guid}")]
        public async Task<ActionResult> ApagarVeiculo([FromRoute] Guid idVeiculo)
        {
            try
            {
                await _veiculoService.Remover(idVeiculo);

                return Ok();
            }
            catch (VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veiculo");
            }
        }
    }
}
