
using System.Linq.Expressions;
using Client.Repositories;

namespace Client.Services;

/// <summary>
/// BaseService - Base service for all services
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TU"></typeparam>
/// <typeparam name="TSelect"></typeparam>
public class BaseService<T, TU, TSelect> : IBaseService<T, TU, TSelect>
    where T : class
    where TSelect : class
{
    protected readonly IBaseRepository<T, TU, TSelect> Repository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    public BaseService(IBaseRepository<T, TU, TSelect> repository)
    {
        Repository = repository;
    }

    public Task<IQueryable<T?>> FindAsync(Expression<Func<T, bool>> predicate = null, bool isTracking = false, params Expression<Func<T, object>>[] includes)
    {
        return Repository.FindAsync(predicate, isTracking, includes);
    }

    /// <summary>
    /// Find multiple records (asynchronous)
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<IQueryable<TSelect?>> FindViewAsync(Expression<Func<TSelect, bool>> predicate = null!,
        bool isTracking = false,
        params Expression<Func<TSelect, object>>[] includes)
    {
        return await Repository.FindViewAsync(predicate, isTracking, includes);
    }

    /// <summary>
    /// Find multiple records
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public IQueryable<TSelect?> FindView(Expression<Func<TSelect, bool>> predicate = null!,
        bool isTracking = false,
        params Expression<Func<TSelect, object>>[] includes)
    {
        return Repository.FindView(predicate, isTracking, includes);
    }

    public IQueryable<T?> Find(Expression<Func<T, bool>> predicate = null, bool isTracking = false, params Expression<Func<T, object>>[] includes)
    {
        return Repository.Find(predicate, isTracking, includes);
    }

    /// <summary>
    /// Get single record by ID (asynchronous)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T?> GetByIdAsync(TU id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public T GetById(TU id)
    {
        return Repository.GetById(id);
    }

    public bool Add(T entity)
    {
        return Repository.Add(entity);
    }

    public void Update(T entity)
    {
        Repository.Update(entity);
    }

    public async Task AddAsync(T entity)
    {
        await Repository.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await Repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Delete entity by ID (asynchronous)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdAsync(TU id)
    {
        return await Repository.DeleteByIdAsync(id);
    }

    public int SaveChanges(string userName, bool needLogicalDelete = false)
    {
        return Repository.SaveChanges(userName, needLogicalDelete);
    }

    public async Task<int> SaveChangesAsync(string userName, bool needLogicalDelete = false)
    {
        return await Repository.SaveChangesAsync(userName, needLogicalDelete);
    }
}
