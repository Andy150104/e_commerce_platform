using System.Linq.Expressions;

namespace Client.Services;

/// <summary>
/// IBaseService - Interface for all services
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
public interface IBaseService<T, U, TRespone> 
    where T : class where TRespone : class
{
    /// <summary>
    /// FindAsync method - Use for get data from database
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IQueryable<T?>> FindAsync(Expression<Func<T, bool>> predicate = null!,
        bool isTracking = false, 
        params Expression<Func<T, object>>[] includes);
    
    /// <summary>
    /// FindViewAsync method - Use for get data from database
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IQueryable<TRespone?>> FindViewAsync(Expression<Func<TRespone, bool>> predicate = null!,
        bool isTracking = false, 
        params Expression<Func<TRespone, object>>[] includes);
    
    /// <summary>
    /// Find method - Use for get data from database
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    IQueryable<TRespone?> FindView(Expression<Func<TRespone, bool>> predicate = null!, 
        bool isTracking = false,  
        params Expression<Func<TRespone, object>>[] includes);
    
    IQueryable<T?> Find(Expression<Func<T, bool>> predicate = null!, 
        bool isTracking = false,  
        params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// GetByIdAsync method - Use for get data by id and use for Update, Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(U id);
    
    T GetById(U id);
    
    bool Add(T entity);

    void Update(T entity);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task<bool> DeleteByIdAsync(U id);
    
    int SaveChanges(string userName, bool needLogicalDelete = false);
    
    Task<int> SaveChangesAsync(string userName, bool needLogicalDelete = false);
}