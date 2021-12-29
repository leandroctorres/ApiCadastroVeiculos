using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroVeiculos.InputModel
{
    public class VeiculoInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "A marca do veículo deve conter entre 2 e 30 caracteres")]
        public string Marca { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "O modelo do veículo deve conter entre 2 e 30 caracteres")]
        public string Modelo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        [Range(1, 999999, ErrorMessage = "O preço deve ser de no minímo 1 real e no máximo 999999 reais")]
        public double Preco { get; set; }
    }
}
