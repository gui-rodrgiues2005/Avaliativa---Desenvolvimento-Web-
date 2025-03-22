using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avaliativa.Models;
using Microsoft.EntityFrameworkCore;


namespace Avaliativa.Controllers
{
    public class EventoController : Controller
    {
        public Context context;

        public EventoController(Context ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var participantes = context.Participantes
                .Include(p => p.Evento) 
                .ToList();

            if (!participantes.Any())
            {
                Console.WriteLine("Nenhum participante encontrado!");
            }

            return View(participantes);
        }

        [HttpPost]
        public IActionResult Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                context.Eventos.Add(evento);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }
    }
}
