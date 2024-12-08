namespace project_2.Controllers;

[Route("")]
public abstract class BaseController<TEntity, TEntityDto>(IRepository repository) : Controller
    where TEntity : cEntity
    where TEntityDto : BaseDto
{
    protected readonly IRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _repository.GetAllAsync<TEntity>());
    }

    [HttpGet("Details/{id}")]// GET: AkkrMintavetel/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (await _repository.GetByIdAsync<TEntity>(id) is not TEntity entity)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View((TEntity)Activator.CreateInstance(typeof(TEntity))!);
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TEntityDto dto)
    {
        if (ModelState.IsValid)
        {
            var now = DateTime.UtcNow;

            var entity = CreateEntity(dto);
            entity.Created = now;
            entity.LastModified = now;

            await _repository.AddAndSaveAsync(entity);

            return RedirectToAction("Index");
        }
        return View(dto);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(long? id)
    {
        if (await _repository.GetByIdAsync<TEntity>(id) is not TEntity entity)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: AkkrMintavetel/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, TEntityDto entityDto)
    {
        if (entityDto.Id == null)
        {
            return BadRequest("Id is missing!");
        }

        if (ModelState.IsValid)
        {
            var entity = await _repository.GetByIdAsync<TEntity>(entityDto.Id);
            if (entity is null)
            {
                return NotFound();
            }

            UpdateEntity(entity, entityDto);
            entity.LastModified = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return RedirectToAction("Edit", new { id });
    }

    [HttpGet("Delete/{id}")]// GET: AkkrMintavetel/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (await _repository.GetByIdAsync<TEntity>(id) is not TEntity entity)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        if (await _repository.GetByIdAsync<TEntity>(id) is TEntity entity)
        {
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    protected abstract TEntity CreateEntity(TEntityDto dto);

    protected abstract TEntity UpdateEntity(TEntity entity, TEntityDto dto);
}
