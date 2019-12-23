using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAtividadeEntrevista.Attributes;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required]
        [CPF]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}\-\d{2}", ErrorMessage = "Formato do CPF inválido")]
        public string CPF { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public long IdCliente { get; set; }
    }
}