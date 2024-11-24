using Domain;
using Microsoft.AspNetCore.Mvc;
using project_2.Dtos;

namespace project_2.Controllers
{
    public class ParameterController : Controller
    {
        private readonly LaborDbContext _context;

        public ParameterController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: Parameter
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parameter.ToListAsync());
        }

        // GET: Parameter/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cParameter = await _context.Parameter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cParameter == null)
            {
                return NotFound();
            }

            return View(cParameter);
        }

        // GET: Parameter/Create
        public IActionResult Create()
        {
            return View(new cParameter());
        }

        // POST: Parameter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParKod,HumviLeiras,SajatLeiras,ParamErtek,ParamTip")] ParameterDto parameterDto)
        {
             if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newParameter = new cParameter
                {
                    ParKod = parameterDto.ParKod,
                    HumviLeiras = parameterDto.HumviLeiras,
                    SajatLeiras = parameterDto.SajatLeiras,
                    ParamErtek = parameterDto.ParamErtek,
                    ParamTip = parameterDto.ParamTip,                  
                    Created = now,
                    LastModified = now
                };
                _context.Add(newParameter);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parameterDto);
        }

        // GET: Parameter/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cParameter = await _context.Parameter.FindAsync(id);
            if (cParameter == null)
            {
                return NotFound();
            }
            return View(cParameter);
        }

        // POST: Parameter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ParKod,HumviLeiras,SajatLeiras,ParamErtek,ParamTip")] ParameterDto parameterDto)
        {
            if (parameterDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var parameter = await _context.Parameter.FirstOrDefaultAsync(x => x.Id == parameterDto.Id);
                    if (parameter != null)
                    {
                        parameter.ParKod = parameterDto.ParKod;
                        parameter.HumviLeiras = parameterDto.HumviLeiras;
                        parameter.SajatLeiras = parameterDto.SajatLeiras;
                        parameter.ParamErtek = parameterDto.ParamErtek;
                        parameter.ParamTip = parameterDto.ParamTip;
                        parameter.LastModified = DateTime.UtcNow;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(parameterDto);

        }

        // GET: Parameter/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cParameter = await _context.Parameter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cParameter == null)
            {
                return NotFound();
            }

            return View(cParameter);
        }

        // POST: Parameter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cParameter = await _context.Parameter.FindAsync(id);
            if (cParameter != null)
            {
                _context.Parameter.Remove(cParameter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cParameterExists(long id)
        {
            return _context.Parameter.Any(e => e.Id == id);
        }
    }
}
