using System.ComponentModel.DataAnnotations.Schema;

namespace FillGaps.WebVendas.WebApp.Entities
{
    [Table("DOCUMENTO_ITEM")]
    public class DocumentoItem
    {
        [ForeignKey("DOCUMENTO")]
        public int IdDocumento { get; set; }
        [ForeignKey("PRODUTO")]
        public int IdProduto { get; set; }
        public decimal QuantidadeProduto { get; set; }
        public decimal ValorUnitarioItem { get; set; }
        public decimal ValorTotalItem { get; set; }
        public Documento Documento { get; set; }
        public Produto Produto { get; set; }
    }
}
