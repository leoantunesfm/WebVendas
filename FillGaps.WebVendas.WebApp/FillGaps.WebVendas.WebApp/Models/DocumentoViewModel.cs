using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FillGaps.WebVendas.WebApp.Models
{
    public class DocumentoViewModel
    {
        public int? IdDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        public DateTime? DataDocumento { get; set; }

        [Required(ErrorMessage = "O campo Cliente é obrigatório.")]
        public int? IdCliente { get; set; }
        public decimal ValorTotalDocumento { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaClientes { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }

        public string JsonProdutos { get; set; }
    }
}

