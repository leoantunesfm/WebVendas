using FillGaps.WebVendas.WebApp.DAL;
using FillGaps.WebVendas.WebApp.Entities;
using FillGaps.WebVendas.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FillGaps.WebVendas.WebApp.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Produto> produtos = _context.Produto
                .Include(c => c.Categoria)
                .ToList();
                                            
            _context.Dispose();
            return View(produtos);
        }

        private IEnumerable<SelectListItem> ListarCategorias()
        {
            List<SelectListItem> listaCategorias = new List<SelectListItem>();

            listaCategorias.Add(new SelectListItem { Value = "0", Text = "Selecione uma categoria" });

            foreach (var categoria in _context.Categoria.ToList())
            {
                listaCategorias.Add(new SelectListItem
                {
                    Value = categoria.IdCategoria.ToString(),
                    Text = categoria.CodigoCategoria.ToString() + " - " + categoria.NomeCategoria.ToString()
                });
            }

            return listaCategorias;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ProdutoViewModel produtoViewModel = new ProdutoViewModel();
            produtoViewModel.ListaCategorias = ListarCategorias();

            if (id != null)
            {
                Produto? produto = _context.Produto.Find(id);
                if (produto != null)
                {
                    produtoViewModel.IdProduto = (int)id;
                    produtoViewModel.CodigoProduto = produto.CodigoProduto;
                    produtoViewModel.DescricaoProduto = produto.DescricaoProduto;
                    produtoViewModel.QuantidadeProduto = produto.QuantidadeProduto;
                    produtoViewModel.PrecoProduto = produto.PrecoProduto;
                    produtoViewModel.IdCategoria = produto.IdCategoria;
                }
            }

            return View(produtoViewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                Produto oProduto = new Produto
                {
                    IdProduto = produtoViewModel.IdProduto,
                    CodigoProduto = produtoViewModel.CodigoProduto,
                    DescricaoProduto = produtoViewModel.DescricaoProduto,
                    QuantidadeProduto = (decimal)produtoViewModel.QuantidadeProduto,
                    PrecoProduto = (decimal)produtoViewModel.PrecoProduto,
                    IdCategoria = (int)produtoViewModel.IdCategoria
                };

                if (oProduto.IdProduto == null)
                {
                    _context.Produto.Add(oProduto);
                }
                else
                {
                    _context.Produto.Update(oProduto);
                }

                _context.SaveChanges();
            }
            else
            {
                produtoViewModel.ListaCategorias = ListarCategorias();
                return View(produtoViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Produto? produto = _context.Produto.Find(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
