namespace Application;

public interface IRepository
{
    ValueTask<T?> GetByIdAsync<T>(long? id) where T : cEntity;

    Task<List<T>> GetAllAsync<T>() where T : cEntity;

    Task AddAndSaveAsync<T>(T entity) where T : cEntity;

    Task SaveChangesAsync();

    void Delete<T>(T entity) where T : cEntity;

    Task TryDeleteByIdAsync<T>(long id) where T : cEntity;
}
