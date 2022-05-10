using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wAppVolvo.Context;

namespace wAppVolvo.Controllers
{
    public class CaminhaosController : Controller
    {
        private readonly CaminhaoDbContext _context;

        public CaminhaosController(CaminhaoDbContext context)
        {
            _context = context;
        }

        // GET: Caminhaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Caminhoes.ToListAsync());
        }

        public List<Caminhao> caminhaos()
        { return _context.Caminhoes.ToList<Caminhao>(); }

        public Caminhao caminhao(int id)
        { return _context.Caminhoes.Find(id); }

        // GET: Caminhaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _context.Caminhoes
                .FirstOrDefaultAsync(m => m.CaminhaoId == id);
            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // GET: Caminhaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caminhaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaminhaoId,Modelo,AnoFabricacao,AnoModelo")] Caminhao caminhao)
        {
            //Modelo(Poderá aceitar apenas FH e FM)
            //Ano de Fabricação(Ano deverá ser o atual)
            //Ano Modelo(Poderá ser o atual ou o ano subsequente)

            ValidateModel(caminhao);
            if (ModelState.IsValid)
            {
                _context.Add(caminhao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(caminhao);
        }

        public void ValidateModel(Caminhao caminhao)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(caminhao.AnoModelo.ToString(), "^[0-9]*$"))
                ModelState.AddModelError("AnoModelo", "AnoModelo Poderá ser o atual ou o ano subsequente");
            else
            {
                if (caminhao.AnoModelo < DateTime.Now.Year)
                    ModelState.AddModelError("AnoModelo", "AnoModelo Poderá ser o atual ou o ano subsequente");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(caminhao.AnoFabricacao.ToString(), "^[0-9]*$"))
                ModelState.AddModelError("AnoFabricacao", "AnoFabricacao deverá ser o atual");
            else
            {
                if (caminhao.AnoFabricacao != DateTime.Now.Year)
                    ModelState.AddModelError("AnoFabricacao", "AnoFabricacao deverá ser o atual");
            }

            if (caminhao.Modelo != null)
            {
                if (caminhao.Modelo != "FM" && caminhao.Modelo != "FH")
                    ModelState.AddModelError("Modelo", "Modelo so permite FM ou FH");
            }
            else
                ModelState.AddModelError("Modelo", "Required");
        }

        // GET: Caminhaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _context.Caminhoes.FindAsync(id);
            if (caminhao == null)
            {
                return NotFound();
            }
            return View(caminhao);
        }

        // POST: Caminhaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaminhaoId,Modelo,AnoFabricacao,AnoModelo")] Caminhao caminhao)
        {
            if (id != caminhao.CaminhaoId)
            {
                return NotFound();
            }

            ValidateModel(caminhao);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caminhao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminhaoExists(caminhao.CaminhaoId))
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
            return View(caminhao);
        }

        // GET: Caminhaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _context.Caminhoes
                .FirstOrDefaultAsync(m => m.CaminhaoId == id);
            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // POST: Caminhaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caminhao = await _context.Caminhoes.FindAsync(id);
            _context.Caminhoes.Remove(caminhao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaminhaoExists(int id)
        {
            return _context.Caminhoes.Any(e => e.CaminhaoId == id);
        }
    }
}
