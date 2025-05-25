using System.ComponentModel.DataAnnotations;

namespace FillGaps.WebVendas.WebApp.Models
{
    public class CategoriaViewModel
    {
        public int? IdCategoria { get; set; }
        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public string CodigoCategoria { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string NomeCategoria { get; set; }
    }
}
