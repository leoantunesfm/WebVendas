namespace FillGaps.WebVendas.WebApp.Models
{
    public class DocumentoConsultaViewModel
    {
        public int IdDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public DateTime DataDocumento { get; set; }
        public ClienteViewModel Cliente { get; set; } = new ClienteViewModel();
        public decimal ValorTotalDocumento { get; set; }
        public IEnumerable<DocumentoItemConsultaViewModel> Itens { get; set; } = new List<DocumentoItemConsultaViewModel>();
    }
}
