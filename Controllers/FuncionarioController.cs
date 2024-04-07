using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InfnetMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace InfnetMVC.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly InfnetDbContext _context;

        public FuncionarioController(InfnetDbContext context)
        {
            _context = context;
        }

        // GET: Funcionario
        [Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> Index()
        {
            var infnetDbContext = _context.Funcionarios.Include(f => f.Departamento);
            return View(await infnetDbContext.ToListAsync());
        }
        
        // GET: Funcionario/Search/Name
        [Authorize(Policy = "Coordenador")]
        [Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> Search(string? nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return View(new List<Funcionario>());
            }

            var funcionarios = await _context.Funcionarios
            .Include(f => f.Departamento)
            .Where(f => f.Nome.ToLower().Contains(nome.ToLower().Trim())) 
            .ToListAsync();


            return View(funcionarios);
        }

        // GET: Funcionario/Details/5
        [Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionario/Create
        [Authorize(Policy = "Coordenador")]
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos.Select(d => new {
                Id = d.Id,
                NomeLocal = $"{d.Nome} - {d.Local}"
            }), "Id", "NomeLocal");

            return View();
        }

        // POST: Funcionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "Coordenador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Telefone,Email,DataDeNascimento,DepartamentoId")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        [Authorize(Policy = "Coordenador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            //ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
            //ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nome");
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos.Select(d => new {
                Id = d.Id,
                NomeLocal = $"{d.Nome} - {d.Local}"
            }), "Id", "NomeLocal");
            return View(funcionario);
        }

        // POST: Funcionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Coordenador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone,Email,DataDeNascimento,DepartamentoId")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
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
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
            return View(funcionario);
        }

        // GET: Funcionario/Delete/5
        [Authorize(Policy = "Coordenador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [Authorize(Policy = "Coordenador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
