using Microsoft.AspNetCore.Mvc;
using Avaliativa.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

public class EventoController : Controller
{
    private readonly Context _context;

    public EventoController(Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var eventos = _context.Eventos.ToList();
        return View(eventos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Evento evento)
    {
         _context.Eventos.Add(evento);
         _context.SaveChanges();
         return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var evento = _context.Eventos
        .Include(e => e.Participante)
        .FirstOrDefault(e => e.EventoId == id);

        return View(evento);
    }

    // Busca o Evento para editar
    public IActionResult Edit(int id)
    {
        var evento = _context.Eventos.Find(id);
        ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Nome), "EventoId", "Nome");
        return View(evento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Evento evento)
    {
        _context.Entry(evento).State = EntityState.Modified;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var evento = _context.Eventos.FirstOrDefault(e => e.EventoId == id);
        _context.Eventos.Remove(evento);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
