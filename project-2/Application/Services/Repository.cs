namespace Application;

public class Repository(LaborDbContext _context) : IRepository
{
    public ValueTask<T?> GetByIdAsync<T>(long? id) where T : cEntity
    {
        if (id is null)
        {
            return default;
        }

        return _context.Set<T>().FindAsync(id);
    }

    public Task<List<T>> GetAllAsync<T>() where T : cEntity
    {
        return _context.Set<T>().ToListAsync();
    }

    public async Task AddAndSaveAsync<T>(T entity) where T : cEntity
    {
        _context.Set<T>().Add(entity);
        await SaveChangesAsync();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Delete<T>(T entity) where T : cEntity
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task TryDeleteByIdAsync<T>(long id) where T : cEntity
    {
        var entity = await GetByIdAsync<T>(id);

        if (entity is null)
        {
            return;
        }

        Delete<T>(entity);
        await SaveChangesAsync();
    }
}
