using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FillGaps.WebVendas.WebApp.Entities
{
    public class Documento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public DateTime DataDocumento { get; set; }
        
        [ForeignKey("CLIENTE")]
        public int IdCliente { get; set; }
        public decimal ValorTotalDocumento { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<DocumentoItem> DocumentoItens { get; set; }
    }
}
