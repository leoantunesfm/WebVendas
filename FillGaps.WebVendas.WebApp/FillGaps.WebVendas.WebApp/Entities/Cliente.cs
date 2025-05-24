using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FillGaps.WebVendas.WebApp.Entities
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CPF_CNPJ { get; set; }
        public string EmailCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public ICollection<Documento> Documentos { get; set; }
    }
}
