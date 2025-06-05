using FillGaps.WebVendas.WebApp.DAL;
using FillGaps.WebVendas.WebApp.Entities;
using FillGaps.WebVendas.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            listaClientes.Add(new SelectListItem { Value = string.Empty, Text = string.Empty });

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

            listaProdutos.Add(new SelectListItem { Value = string.Empty, Text = string.Empty });

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

        [HttpGet]
        public IActionResult Consulta(int? id)
        {
            DocumentoConsultaViewModel documentoConsultaViewModel = new DocumentoConsultaViewModel();

            if (id != null)
            {
                Documento? documento = _context.Documento
                                        .Include(c => c.Cliente)
                                        .Include(i => i.DocumentoItens)
                                        .ThenInclude(p => p.Produto)
                                        .FirstOrDefault(d => d.IdDocumento == id);
                                        
                if (documento != null)
                {
                    documentoConsultaViewModel.IdDocumento = (int)id;
                    documentoConsultaViewModel.NumeroDocumento = documento.NumeroDocumento;
                    documentoConsultaViewModel.DataDocumento = documento.DataDocumento;
                    documentoConsultaViewModel.ValorTotalDocumento = documento.ValorTotalDocumento;

                    Cliente? cliente = documento.Cliente;

                    if (cliente != null)
                    {
                        documentoConsultaViewModel.Cliente.IdCliente = cliente.IdCliente;
                        documentoConsultaViewModel.Cliente.CodigoCliente = cliente.CodigoCliente;
                        documentoConsultaViewModel.Cliente.NomeCliente = cliente.NomeCliente;
                    }

                    List<DocumentoItemConsultaViewModel> listaItens = new List<DocumentoItemConsultaViewModel>();

                    foreach (var item in documento.DocumentoItens)
                    {
                        DocumentoItemConsultaViewModel itemConsulta = new DocumentoItemConsultaViewModel
                        {
                            IdDocumento = (int)item.IdDocumento,
                            QuantidadeProduto = item.QuantidadeProduto,
                            ValorUnitarioItem = item.ValorUnitarioItem,
                            ValorTotalItem = item.ValorTotalItem
                        };

                        Produto? produto = _context.Produto.Find(item.IdProduto);

                        if (produto != null)
                        {
                            itemConsulta.Produto.IdProduto = (int)produto.IdProduto;
                            itemConsulta.Produto.CodigoProduto = produto.CodigoProduto;
                            itemConsulta.Produto.DescricaoProduto = produto.DescricaoProduto;
                        }

                        listaItens.Add(itemConsulta);
                    }

                    documentoConsultaViewModel.Itens = listaItens;

                }

            }

            return View(documentoConsultaViewModel);
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
                        ? JsonConvert.DeserializeObject<ICollection<DocumentoItem>>(documentoViewModel.JsonProdutos) //System.Text.Json.JsonSerializer.Deserialize<ICollection<DocumentoItem>>(documentoViewModel.JsonProdutos)
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
            List<DocumentoItem>? documentoItem = _context.DocumentoItem.Where(di => di.IdDocumento == id).ToList();
            if (documento != null && documentoItem != null)
            {
                foreach (var item in documentoItem)
                {
                    _context.DocumentoItem.Remove(item);
                }
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
