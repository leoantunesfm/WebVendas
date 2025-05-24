using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FillGaps.WebVendas.WebApp.Entities
{
    [Table("PRODUTO")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduto { get; set; }
        public string CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal QuantidadeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        [ForeignKey("CATEGORIA")]
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<DocumentoItem> DocumentoItens { get; set; }
    }
}
