using System.Linq.Expressions;

namespace Client.Repositories;

/// <summary>
/// IBaseRepository - Interface for all repositories
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TU"></typeparam>
public interface IBaseRepository<T, TU, TSelect>
    where T : class
    where TSelect : class
{
    // Find multiple records (synchronous)
    IQueryable<TSelect?> FindView(Expression<Func<TSelect, bool>> predicate = null!, 
        bool isTracking = false,  
        params Expression<Func<TSelect, object>>[] includes);
    
    IQueryable<T?> Find(Expression<Func<T, bool>> predicate = null!, 
        bool isTracking = true,  
        params Expression<Func<T, object>>[] includes);

    // Find multiple records (asynchronous)
    Task<IQueryable<TSelect?>> FindViewAsync(Expression<Func<TSelect, bool>> predicate = null!, 
        bool isTracking = false, 
        params Expression<Func<TSelect, object>>[] includes);
    
    
    Task<IQueryable<T?>> FindAsync(Expression<Func<T, bool>> predicate = null!, 
        bool isTracking = true, 
        params Expression<Func<T, object>>[] includes);

    // Get single record by ID (synchronous)
    T? GetById(TU id);

    // Get single record by ID (asynchronous)
    Task<T> GetByIdAsync(TU id);

    // Add entity (synchronous)
    bool Add(T entity);

    // Add entity (asynchronous)
    Task AddAsync(T entity);

    // Update entity (synchronous)
    void Update(T entity);

    // Update entity (asynchronous)
    Task UpdateAsync(T entity);

    // Delete entity by ID (synchronous)
    bool DeleteById(TU id);

    // Delete entity by ID (asynchronous)
    Task<bool> DeleteByIdAsync(TU id);

    // 
    int SaveChanges(string userName, bool needLogicalDelete = false);
    
    Task<int> SaveChangesAsync(string userName, bool needLogicalDelete = false);

    public void ExecuteInTransaction(Action action);
    
    public Task ExecuteInTransactionAsync(Func<Task> action);
}
