using FillGaps.WebVendas.WebApp.DAL;
using FillGaps.WebVendas.WebApp.Entities;
using FillGaps.WebVendas.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FillGaps.WebVendas.WebApp.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DocumentoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Documento> documentos = _context.Documento
                .Include(d => d.Cliente)
                .ToList();

            _context.Dispose();

            return View(documentos);
        }

        private IEnumerable<SelectListItem> ListarClientes()
        {
            List<SelectListItem> listaClientes = new List<SelectListItem>();

            listaClientes.Add(new SelectListItem { Value = "0", Text = "Selecione um Cliente" });

            foreach (var cliente in _context.Cliente.ToList())
            {
                listaClientes.Add(new SelectListItem
                {
                    Value = cliente.IdCliente.ToString(),
                    Text = cliente.CodigoCliente.ToString() + " - " + cliente.NomeCliente.ToString()
                });
            }

            return listaClientes;
        }

        private IEnumerable<SelectListItem> ListarProdutos()
        {
            List<SelectListItem> listaProdutos = new List<SelectListItem>();

            listaProdutos.Add(new SelectListItem { Value = "0", Text = "Selecione um produto" });

            foreach (var produto in _context.Produto.ToList())
            {
                listaProdutos.Add(new SelectListItem
                {
                    Value = produto.IdProduto.ToString(),
                    Text = produto.CodigoProduto.ToString() + " - " + produto.DescricaoProduto.ToString()
                });
            }

            return listaProdutos;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            DocumentoViewModel documentoViewModel = new DocumentoViewModel();
            documentoViewModel.ListaClientes = ListarClientes();
            documentoViewModel.ListaProdutos = ListarProdutos();

            if (id != null)
            {
                Documento? documento = _context.Documento.Find(id);

                if (documento != null)
                {
                    documentoViewModel.IdDocumento = (int)id;
                    documentoViewModel.NumeroDocumento = documento.NumeroDocumento;
                    documentoViewModel.DataDocumento = documento.DataDocumento;
                    documentoViewModel.IdCliente = documento.IdCliente;
                    documentoViewModel.ValorTotalDocumento = documento.ValorTotalDocumento;
                }
            }

            return View(documentoViewModel);
        }

        public int GerarNumeroDocumento()
        {
            int numeroDocumento = 1;
            if (_context.Documento.Any())
            {
                numeroDocumento = _context.Documento.Max(d => d.NumeroDocumento) + 1;
            }

            return numeroDocumento;
        }

        [HttpPost]
        public IActionResult Cadastro(DocumentoViewModel documentoViewModel)
        {
            if (ModelState.IsValid)
            {
                Documento oDocumento = new Documento
                {
                    IdDocumento = documentoViewModel.IdDocumento,
                    NumeroDocumento = documentoViewModel.NumeroDocumento,
                    DataDocumento = (DateTime)documentoViewModel.DataDocumento,
                    IdCliente = (int)documentoViewModel.IdCliente,
                    ValorTotalDocumento = documentoViewModel.ValorTotalDocumento,
                    DocumentoItens = documentoViewModel.JsonProdutos != null
                        ? System.Text.Json.JsonSerializer.Deserialize<ICollection<DocumentoItem>>(documentoViewModel.JsonProdutos)
                        : new List<DocumentoItem>()

                };

                if (oDocumento.IdDocumento == null)
                {
                    oDocumento.NumeroDocumento = GerarNumeroDocumento();
                    _context.Documento.Add(oDocumento);
                }
                else
                {
                    _context.Documento.Update(oDocumento);
                }

                _context.SaveChanges();
            }
            else
            {
                documentoViewModel.ListaClientes = ListarClientes();
                documentoViewModel.ListaProdutos = ListarProdutos();
                return View(documentoViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Documento? documento = _context.Documento.Find(id);
            if (documento != null)
            {
                _context.Documento.Remove(documento);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet("ObterPrecoProduto/{IdProduto}")]
        public decimal ObterPrecoProduto(int IdProduto)
        {
            decimal precoProduto = 0;
            Produto? produto = _context.Produto.Find(IdProduto);
            if (produto != null)
            {
                precoProduto = produto.PrecoProduto;
            }
            return precoProduto;
        }
    }
}
