using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreateDb.Model;

namespace CreateDb.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly StudentStore _studentStore;
       
        public StudentController(SchoolContext context, StudentStore store)
        {
            _context = context;
            _studentStore = store;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Students.ToListAsync());
        }
        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            await _studentStore.CreateAsync(student);
            return Ok(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        
        public async Task<IActionResult> Edit([FromRoute]int? id, [FromBody] Student student)
        {
            if (id.HasValue)
            {
                student.Id = id.Value; 
            }
            if (student.Id <= 0)
            {
                return BadRequest("O ID do estudante é obrigatório.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentStore.UpdateAsync(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return BadRequest(ModelState);
        }

        // GET: Student/Delete
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            var student = await _studentStore.FindByIdAsync(id);
            if (student != null)
            {
                _studentStore.DeleteAsync(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
