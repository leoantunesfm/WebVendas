namespace FillGaps.WebVendas.WebApp.Models
{
    public class DocumentoItemConsultaViewModel
    {
        public int IdDocumento { get; set; }
        public ProdutoViewModel Produto { get; set; } = new ProdutoViewModel();
        public decimal QuantidadeProduto { get; set; }
        public decimal ValorUnitarioItem { get; set; }
        public decimal ValorTotalItem { get; set; }
    }
}
