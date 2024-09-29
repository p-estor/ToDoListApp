using Microsoft.AspNetCore.Mvc;
using TaskManagerCore.Data;
using TaskManagerCore.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerCore.Extensions;

public class TareasController : Controller
{
    private readonly AppDbContext _context;

    public TareasController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search, int pageNumber = 1, int pageSize = 5)
    {
        var tareas = _context.Tareas.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            tareas = tareas.Where(s => s.Title.Contains(search));
        }

        var totalItems = await tareas.CountAsync();
        var paginadas = await tareas.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        var paginacionViewModel = new PaginacionViewModel
        {
            Tareas = paginadas,
            Paginacion = new Paginacion
            {
                CurrentPage = pageNumber,
                TotalItems = totalItems,
                ItemsPerPage = pageSize
            }
        };

        // Si la solicitud es AJAX, devuelve sólo la vista parcial
        if (Request.IsAjax())
        {
            return PartialView("_TareasPartial", paginacionViewModel.Tareas);
        }

        return View(paginacionViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tareas tarea)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tarea);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea == null)
        {
            return NotFound();
        }
        return View(tarea);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Tareas tarea)
    {
        if (id != tarea.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tarea);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tarea = await _context.Tareas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tarea == null)
        {
            return NotFound();
        }

        return View(tarea);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        _context.Tareas.Remove(tarea);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Route("Tareas/Completar/{id}")]
    public async Task<IActionResult> MarkAsCompleted(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);

        if (tarea == null)
        {
            return NotFound();
        }

        tarea.IsCompleted = !tarea.IsCompleted; // Marcar como completada
        _context.Update(tarea);
        await _context.SaveChangesAsync();

        //return RedirectToAction(nameof(Index));

        // Devuelve el estado de la tarea como respuesta JSON
        return Json(new { isCompleted = tarea.IsCompleted });
    }
}
