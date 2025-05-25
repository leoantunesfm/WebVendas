using FillGaps.WebVendas.WebApp.DAL;
using FillGaps.WebVendas.WebApp.Entities;
using FillGaps.WebVendas.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.WebVendas.WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Categoria> categorias = _context.Categoria.ToList();
            _context.Dispose();
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel categoriaViewModel = new CategoriaViewModel();

            if (id != null)
            {
                Categoria? categoria = _context.Categoria.Find(id);
                //var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
                //var categoria = _context.Categoria.Where(c => c.IdCategoria == id).FirstOrDefault();
                if (categoria != null)
                {
                    categoriaViewModel.IdCategoria = (int)id;
                    categoriaViewModel.CodigoCategoria = categoria.CodigoCategoria;
                    categoriaViewModel.NomeCategoria = categoria.NomeCategoria;
                }
            }

            return View(categoriaViewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel categoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                Categoria oCategoria = new Categoria
                {
                    IdCategoria = categoriaViewModel.IdCategoria,
                    CodigoCategoria = categoriaViewModel.CodigoCategoria,
                    NomeCategoria = categoriaViewModel.NomeCategoria
                };

                if (oCategoria.IdCategoria == null)
                {
                    _context.Categoria.Add(oCategoria);
                }
                else
                {
                    _context.Categoria.Update(oCategoria);
                }

                _context.SaveChanges();
            }
            else
            {
                return View(categoriaViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Categoria? categoria = _context.Categoria.Find(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
