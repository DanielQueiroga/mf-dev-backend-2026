
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mf_dev_backend_2026.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class UsuariosController : Controller
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: USUARIOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Usuarios.ToListAsync());
    }

    // Login de Usuário
    public IActionResult Login()
    {
        return View();
    }


    //Validação dos dados inseridos
    [HttpPost]
    public async Task<IActionResult> Login(Usuario usuario)
    {

        // Variavel dados para receber o ID do usuario
        var dados = await _context.Usuarios
            .FindAsync(usuario.Id);

        if (dados == null)
        {
            //ID não encontrado
            ViewBag.Message = "Usuário e/ou inválidos.";
            return View();
        }

        // variavel SenhaOk recebe a senha do usuario
        bool senhaOk = BCrypt.Net.BCrypt.Verify(usuario.Senha, dados.Senha);

        if(senhaOk)
        {   //Senha correta
            //Auteticação do usuário e criação da credenciais do usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dados.Nome),
                new Claim(ClaimTypes.NameIdentifier, dados.Id.ToString),
                new Claim(ClaimTypes.Role, dados.Perfil.ToString)
            };

            // Criar uma identidade
            var usuarioIdetity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdetity);

            //Deixa logado por 8 horas
            var props = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(8),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(principal, props);

            return Redirect("/");
        }
        else
        {   //Senha incorreta
            ViewBag.Message = "Usuário e/ou inválidos."
        }
        return View();
    }



    // GET: USUARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: USUARIOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: USUARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Senha,Perfil")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            // Vai transformar a senha livre em uma senha criptografada
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    // GET: USUARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Senha,Perfil")] Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Vai transformar a senha livre em uma senha criptografada
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    // GET: USUARIOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // POST: USUARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int? id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }
}
