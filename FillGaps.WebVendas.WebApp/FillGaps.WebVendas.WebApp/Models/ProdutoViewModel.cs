using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace FillGaps.WebVendas.WebApp.Models
{
    public class ProdutoViewModel
    {
        public int? IdProduto { get; set; }

        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public string CodigoProduto { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string DescricaoProduto { get; set; }

        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "A quantidade deve ser um valor positivo.")]
        public decimal? QuantidadeProduto { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal? PrecoProduto { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
        public int? IdCategoria { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
