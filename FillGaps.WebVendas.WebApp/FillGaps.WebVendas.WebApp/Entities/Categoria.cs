using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FillGaps.WebVendas.WebApp.Entities
{
    [Table("CATEGORIA")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdCategoria { get; set; }
        public string CodigoCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
