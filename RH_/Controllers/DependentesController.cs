using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RH_.Models;

namespace RH_.Controllers
{
    public class DependentesController : Controller
    {
        private readonly AppDbContext _context;

        public DependentesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dependentes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Dependentes.Include(d => d.MatriculaFuncionarioNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Dependentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependentes = await _context.Dependentes
                .Include(d => d.MatriculaFuncionarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependentes == null)
            {
                return NotFound();
            }

            return View(dependentes);
        }

        // GET: Dependentes/Create
        public IActionResult Create()
        {
            ViewData["MatriculaFuncionario"] = new SelectList(_context.Funcionarios, "MatriculaFuncionario","MatriculaFuncionario");// "Bairro"
            return View();
        }

        // POST: Dependentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MatriculaFuncionario,Cpf,Nome,DataNascimento,Sexo")] Dependentes dependentes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependentes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatriculaFuncionario"] = new SelectList(_context.Funcionarios, "MatriculaFuncionario", "MatriculaFuncionario", dependentes.MatriculaFuncionario);// "Bairro"
            return View();
            return View(dependentes);
        }

        // GET: Dependentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependentes = await _context.Dependentes.FindAsync(id);
            if (dependentes == null)
            {
                return NotFound();
            }
            ViewData["MatriculaFuncionario"] = new SelectList(_context.Funcionarios, "MatriculaFuncionario", "MatriculaFuncionario", dependentes.MatriculaFuncionario);// "Bairro"
            return View(dependentes);
        }

        // POST: Dependentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatriculaFuncionario,Cpf,Nome,DataNascimento,Sexo")] Dependentes dependentes)
        {
            if (id != dependentes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependentes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependentesExists(dependentes.Id))
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
            ViewData["MatriculaFuncionario"] = new SelectList(_context.Funcionarios, "MatriculaFuncionario", "MatriculaFuncionario", dependentes.MatriculaFuncionario);//"Bairro"
            return View(dependentes);
        }

        // GET: Dependentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependentes = await _context.Dependentes
                .Include(d => d.MatriculaFuncionarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependentes == null)
            {
                return NotFound();
            }

            return View(dependentes);
        }

        // POST: Dependentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependentes = await _context.Dependentes.FindAsync(id);
            _context.Dependentes.Remove(dependentes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependentesExists(int id)
        {
            return _context.Dependentes.Any(e => e.Id == id);
        }
    }
}
