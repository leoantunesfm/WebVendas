using FillGaps.WebVendas.WebApp.DAL;
using FillGaps.WebVendas.WebApp.Entities;
using FillGaps.WebVendas.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.WebVendas.WebApp.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Cliente> clientes = _context.Cliente.ToList();
            _context.Dispose();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ClienteViewModel clienteViewModel = new ClienteViewModel();

            if (id != null)
            {
                Cliente? cliente = _context.Cliente.Find(id);
                if (cliente != null)
                {
                    clienteViewModel.IdCliente = (int)id;
                    clienteViewModel.CodigoCliente = cliente.CodigoCliente;
                    clienteViewModel.NomeCliente = cliente.NomeCliente;
                    clienteViewModel.CPF_CNPJ = cliente.CPF_CNPJ;
                    clienteViewModel.EmailCliente = cliente.EmailCliente;
                    clienteViewModel.TelefoneCliente = cliente.TelefoneCliente;
                }
            }

            return View(clienteViewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente oCliente = new Cliente
                {
                    IdCliente = clienteViewModel.IdCliente,
                    CodigoCliente = clienteViewModel.CodigoCliente,
                    NomeCliente = clienteViewModel.NomeCliente,
                    CPF_CNPJ = clienteViewModel.CPF_CNPJ,
                    EmailCliente = clienteViewModel.EmailCliente,
                    TelefoneCliente = clienteViewModel.TelefoneCliente
                };

                if (oCliente.IdCliente == null)
                {
                    _context.Cliente.Add(oCliente);
                }
                else
                {
                    _context.Cliente.Update(oCliente);
                }

                _context.SaveChanges();
            }
            else
            {
                return View(clienteViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Cliente? cliente = _context.Cliente.Find(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
