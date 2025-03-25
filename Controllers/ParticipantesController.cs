using Avaliativa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Avaliativa.Controllers
{
    public class ParticipantesController : Controller
    {
        private readonly Context _context;

        public ParticipantesController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var participantes = _context.Participantes
                .Include(p => p.Evento)
                .ToList();
            return View(participantes);
        }

        public IActionResult Create()
        {
            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Nome), "EventoId", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Participante participante)
        {
            _context.Add(participante);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int id)
        {
            var participante = _context.Participantes
                .Include(p => p.Evento)
                .FirstOrDefault(p => p.Id == id);
            return View(participante);
        }
    
        public IActionResult Edit(int id)
        {
            var participante = _context.Participantes.Find(id);
            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Nome), "EventoId", "Nome");
            return View(participante);
        }

        [HttpPost]
        public IActionResult Edit(Participante participante)
        {
            _context.Entry(participante).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var participante = _context.Participantes
                .Include(p => p.Evento)
                .FirstOrDefault(p => p.Id== id);
            return View(participante);
        }

        [HttpPost]
        public IActionResult Delete(Participante participante)
        {
            _context.Participantes.Remove(participante);
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }


        // Adicionando o método no controlador para gerar o certificado
        public IActionResult GenerateCertificate(int id)
        {
            // Recupera o participante e evento associados
            var participante = _context.Participantes
                .Include(p => p.Evento)
                .FirstOrDefault(p => p.Id == id);

            if (participante == null)
            {
                return NotFound();
            }

            // Aqui você pode gerar o certificado, seja um PDF ou uma simples página com o certificado.
            // Por exemplo, gerar uma página HTML de certificado simples:

            var certificadoModel = new CertificadoModel
            {
                NomeParticipante = participante.Nome,
                NomeEvento = participante.Evento.Nome,
                DataConclusao = DateTime.Now
            };

            return View("Certificado", certificadoModel);
        }

    }
}
