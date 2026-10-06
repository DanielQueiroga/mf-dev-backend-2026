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

        //------------------------TELA INDEX--------------------------------------//
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

        //----------------------------TELA DE CADASTRO DE VEICULOS------------------------------------//
        // Criar a tela para cadastro dos dados
        // O primeiro será o metodo GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // O Segundo sera o metodo Post
        // Vai receber a descrição do formulario
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            // Verificando se o modelo de dados é valido
            // Vai verificar se o campo está preenchido ou não
            if (ModelState.IsValid)
            {
                // Se o campo for preenchido, os dados serão adicionado na tabela
                // Adicionara o novo Veiculo ao Banco de dados
                _context.Veiculos.Add(veiculo);
                await _context.SaveChangesAsync();
                //Vai retornar para a pagina Index
                return RedirectToAction("Index");
            }

            return View( veiculo );
        }

        //----------------------------TELA DE EDIÇÃO------------------------------------//
    }
}
