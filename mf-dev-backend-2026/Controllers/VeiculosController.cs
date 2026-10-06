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
        // Recebe a variavel id para receber a rota do veiculo que sera editado
        // Ele vai pegar(get) os dados de acordo com o id no banco de dados e retorna eles como formulário
        public async Task<IActionResult> Edit(int? id)
        {
            // Se não tiver id não retorna nada 
            if(id == null)
            {
                return NotFound();
            }
            // vai procurar o veiculo pelo id dele
            var dados = await _context.Veiculos.FindAsync(id);

            // Se não encontrar os dados retorno NotFound
            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
            // o id da rota deve ser o mesmo do Id do veiculo
            if (id != veiculo.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Com os dados preenchidos ele vai update(ataulizar) os dados do banco de dados
                _context.Veiculos.Update(veiculo);
                // Salva no banco de dados
                await _context.SaveChangesAsync();
                //Vai retornar para a pagina Index
                return RedirectToAction("Index");
            }

            return View();
        }


        //----------------------------TELA DE VISUALIZAR DADOS------------------------------------//
        // Ele pega(get) as informações do veiculo
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            // dados vai receber os dados do veiculo no banco de dados
            var dados = await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }


        //----------------------------TELA DE APAGAR------------------------------------//
        // Pega os dados igual Details
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            // dados vai receber os dados do veiculo no banco de dados
            var dados = await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            // dados vai receber os dados do veiculo no banco de dados
            var dados = await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();

            //Remover o veiculo do banco de dados
            _context.Veiculos.Remove(dados);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }

}
