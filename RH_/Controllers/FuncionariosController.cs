using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH_.Models;

namespace RH_.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.MatriculaFuncionario == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MatriculaFuncionario,Cpf,Nome,DataNascimento,Telefone,Sexo,Cep,Endereco,Numero,Complemento,Bairro,Cidade,Uf,Ativo")] Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatriculaFuncionario,Cpf,Nome,DataNascimento,Telefone,Sexo,Cep,Endereco,Numero,Complemento,Bairro,Cidade,Uf,Ativo")] Funcionarios funcionarios)
        {
            if (id != funcionarios.MatriculaFuncionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionariosExists(funcionarios.MatriculaFuncionario))
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
            return View(funcionarios);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.MatriculaFuncionario == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionarios = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionariosExists(int id)
        {
            return _context.Funcionarios.Any(e => e.MatriculaFuncionario == id);
        }

        public async Task<IActionResult> List()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }

        public IActionResult Aniversario()
        {

            var aniverFunc = _context.Funcionarios.FromSqlRaw("Select id,matriculafuncionario,cpf,telefone,sexo,cep,endereco,numero,complemento,bairro,cidade,uf,ativo, nome,datanascimento FROM funcionarios f WHERE DATEPART(month, f.datanascimento) = datepart(month, getdate())").ToList();

      
            if ((aniverFunc == null) )
            {
                return NotFound();
            }

            return View(aniverFunc);
        }



        public IActionResult AniversarioDep()
        {

            var aniverDep = _context.Dependentes.FromSqlRaw("Select id,matriculafuncionario,cpf,sexo,nome,datanascimento FROM dependentes d WHERE DATEPART(month, d.datanascimento) = datepart(month, getdate())").ToList();


            if ((aniverDep == null))
            {
                return NotFound();
            }

            return View(aniverDep);
        }



    }
}
