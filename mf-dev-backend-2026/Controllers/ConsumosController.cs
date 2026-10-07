using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mf_dev_backend_2026.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ConsumosController : Controller
{
    private readonly AppDbContext _context;

    public ConsumosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: CONSUMOS
    public async Task<IActionResult> Index()
    {
        var consumos = _context.Consumos
            .Include(c => c.Veiculo);

        return View(await consumos.ToListAsync());
    }

    // GET: CONSUMOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consumo = await _context.Consumos
            .Include(c => c.Veiculo)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (consumo == null)
        {
            return NotFound();
        }

        return View(consumo);
    }

    // GET: CONSUMOS/Create
    public IActionResult Create()
    {
        ViewData["VeiculoId"] = new SelectList(
            _context.Veiculos,
            "Id",
            "Nome"
        );

        return View();
    }

    // POST: CONSUMOS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Descricao,Data,Valor,Km,Tipo,VeiculoId")] Consumo consumo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(consumo);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["VeiculoId"] = new SelectList(
            _context.Veiculos,
            "Id",
            "Nome",
            consumo.VeiculoId
        );

        return View(consumo);
    }

    // GET: CONSUMOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consumo = await _context.Consumos.FindAsync(id);

        if (consumo == null)
        {
            return NotFound();
        }

        ViewData["VeiculoId"] = new SelectList(
            _context.Veiculos,
            "Id",
            "Nome",
            consumo.VeiculoId
        );

        return View(consumo);
    }

    // POST: CONSUMOS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Descricao,Data,Valor,Km,Tipo,VeiculoId")] Consumo consumo)
    {
        if (id != consumo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(consumo);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumoExists(consumo.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["VeiculoId"] = new SelectList(
            _context.Veiculos,
            "Id",
            "Nome",
            consumo.VeiculoId
        );

        return View(consumo);
    }

    // GET: CONSUMOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consumo = await _context.Consumos
            .Include(c => c.Veiculo)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (consumo == null)
        {
            return NotFound();
        }

        return View(consumo);
    }

    // POST: CONSUMOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var consumo = await _context.Consumos.FindAsync(id);

        if (consumo != null)
        {
            _context.Consumos.Remove(consumo);

            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ConsumoExists(int id)
    {
        return _context.Consumos.Any(e => e.Id == id);
    }
}