using mf_dev_backend_2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_backend_2026.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;
        public VeiculosController(AppDbContext context)
        {
            // Utilizaremos a variavel context para mexer no banco de dados
            _context = context;
        }

        // Criar classes de interação do usuario
        // Index sera criado primeiro, retorna todos os dados da tabela
        // INDEX SERÁ UMA PAGINA
        public async Task<IActionResult> Index()
        {
            // Variavel para receber os dados do veiculo
            var dados = await _context.Veiculos.ToListAsync();

            // Retornaram os dados na view
            return View(dados); 
        }
    }
}
