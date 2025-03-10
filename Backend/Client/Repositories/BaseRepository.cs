using System.Linq.Expressions;
using Client.Models.Helper;
using Microsoft.EntityFrameworkCore;

namespace Client.Repositories;

/// <summary>
/// BaseRepository - Base class for all repositories
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="Type"></typeparam>
public class BaseRepository<TEntity, Type, TSelect> : IBaseRepository<TEntity, Type, TSelect>
    where TEntity : class
    where TSelect : class
{
    protected readonly AppDbContext Context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    private DbSet<TEntity> DbSet => Context.Set<TEntity>();

    private DbSet<TSelect> DbSetSelect => Context.Set<TSelect>();

    /// <summary>
    /// Find multiple records - Use for select
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IQueryable<TSelect?> FindView(Expression<Func<TSelect, bool>> predicate = null, bool isTracking = false,
        params Expression<Func<TSelect, object>>[] includes)
    {
        IQueryable<TSelect> query = DbSetSelect;

        // Predicate
        if (predicate != null) query = query.Where(predicate);

        // Include
        if (includes != null) query = includes.Aggregate(query, (current, inc) => current.Include(inc));

        // No tracking
        if (!isTracking) query = query.AsNoTracking();

        return query;
    }

    /// <summary>
    /// Find multiple records - Use for select
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public IQueryable<TEntity?> Find(Expression<Func<TEntity, bool>> predicate = null, bool isTracking = false,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        if (predicate != null) query = query.Where(predicate);

        if (includes != null) query = includes.Aggregate(query, (current, inc) => current.Include(inc));

        if (!isTracking) query = query.AsNoTracking();

        return query;
    }

    /// <summary>
    /// Find multiple records (asynchronous) - Use for select
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="isTracking"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public Task<IQueryable<TSelect?>> FindViewAsync(Expression<Func<TSelect, bool>> predicate = null,
        bool isTracking = false,
        params Expression<Func<TSelect, object>>[] includes)
    {
        IQueryable<TSelect> query = DbSetSelect;
        if (predicate != null) query = query.Where(predicate);

        if (includes != null) query = includes.Aggregate(query, (current, inc) => current.Include(inc));

        if (!isTracking) query = query.AsNoTracking();

        return Task.FromResult(query);
    }

    public Task<IQueryable<TEntity?>> FindAsync(Expression<Func<TEntity, bool>> predicate = null,
        bool isTracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;
        if (predicate != null) query = query.Where(predicate);

        if (includes != null) query = includes.Aggregate(query, (current, inc) => current.Include(inc));

        if (!isTracking) query = query.AsNoTracking();

        return Task.FromResult(query);
    }

    /// <summary>
    /// Get entity by Id - Use for Insert, Update, Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TEntity? GetById(Type id)
    {
        return DbSet.Find(id);
    }

    /// <summary>
    /// Get entity by Id asynchronously - Use for Insert, Update, Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity?> GetByIdAsync(Type id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    /// Add entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Add(TEntity entity)
    {
        Context.Add(entity);
        return true;
    }

    /// <summary>
    /// Add entity asynchronously
    /// </summary>
    public async Task AddAsync(TEntity entity)
    {
        Context.AddAsync(entity);
    }

    /// <summary>
    /// Update entity
    /// </summary>
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// Update entity asynchronously
    /// </summary>
    public Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool DeleteById(Type id)
    {
        var entity = GetById(id);
        if (entity == null) return false;

        DbSet.Update(entity);
        return true;
    }

    /// <summary>
    /// Delete entity by ID asynchronously
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdAsync(Type id)
    {
        var entity = await GetByIdAsync(id);

        // Check if entity is null
        if (entity == null) return false;

        DbSet.Update(entity);
        return true;
    }

    /// <summary>
    /// Save changes
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="needLogicalDelete"></param>
    /// <exception cref="NotImplementedException"></exception>
    public int SaveChanges(string userName, bool needLogicalDelete = false)
    {
        return Context.SaveChanges(userName, needLogicalDelete);
    }

    /// <summary>
    /// Save changes asynchronously
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="needLogicalDelete"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> SaveChangesAsync(string userName, bool needLogicalDelete = false)
    {
        return await Context.SaveChangesAsync(userName, needLogicalDelete);
    }

    /// <summary>
    /// Execute multiple operations within a transaction.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public void ExecuteInTransaction(Action action)
    {
        using (var transaction = Context.Database.BeginTransaction())
        {
                action();
                transaction.Commit();
        }
    }

    /// <summary>
    /// Execute multiple operations within a transaction asynchronously.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        using (var transaction = await Context.Database.BeginTransactionAsync())
        {
            await action();
            await transaction.CommitAsync();
        }
    }
}