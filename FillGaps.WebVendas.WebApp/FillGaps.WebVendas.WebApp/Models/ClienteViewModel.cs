using FillGaps.WebVendas.WebApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace FillGaps.WebVendas.WebApp.Models
{
    public class ClienteViewModel
    {
        public int? IdCliente { get; set; }

        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public string CodigoCliente { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "O campo CPF/CNPJ é obrigatório.")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string EmailCliente { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string TelefoneCliente { get; set; }
    }
}
