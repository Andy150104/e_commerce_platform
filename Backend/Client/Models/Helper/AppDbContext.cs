using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Client.SystemClient;
using server.Models;

namespace Client.Models.Helper;

/// <summary>
/// AppDbContext
/// </summary>
public class AppDbContext : BBExTradingFloorContext
{
    public NLog.Logger _Logger;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(new DbContextOptions<BBExTradingFloorContext>())
    {
    }
    
     public User User { get; set; }
    
    public IdentityEntity IdentityEntity { get; set; }
    
    
    /// <summary>
    /// Save changes with common value
    /// </summary>
    /// <param name="updateFunctionId"></param>
    /// <param name="updateUserId"></param>
    /// <param name="needLogicalDelete"></param>
    /// <returns></returns>
    public int SaveChanges(string updateUserId, bool needLogicalDelete = false)
    {
        this.SetCommonValue(updateUserId, needLogicalDelete);
        return base.SaveChanges();
    }
    
    /// <summary>
    /// Set common value for all entities
    /// </summary>
    /// <param name="updateFunctionId"></param>
    /// <param name="updateUser"></param>
    /// <param name="needLogicalDelete"></param>
    private void SetCommonValue(string updateUser, bool needLogicalDelete = false)
    {

        // Register
        var newEntities = this.ChangeTracker.Entries()
            .Where(
                x => x.State == EntityState.Added &&
                x.Entity != null
                )
            .Select(e => e.Entity);

        // Modify
        var modifiedEntities = this.ChangeTracker.Entries()
            .Where(
                x => x.State == EntityState.Modified &&
                    x.Entity != null
                    )
                .Select(e => e.Entity);

        // Get current time
        var now = System.DateTime.Now;
        // Add
        foreach (dynamic newEntity in newEntities)
        {
            try
            {
                newEntity.IsActive = true;
                newEntity.CreatedAt = now;
                newEntity.CreatedBy = updateUser;
                newEntity.UpdatedBy = updateUser;
                newEntity.UpdatedAt = now;
            }
            catch (IOException e)
            {
                // There may be no elements, so don't throw an error
                _Logger.Error(e);
            }
        }

        // Set modifiedEntities
        foreach (dynamic modifiedEntity in modifiedEntities)
        {
            try
            {
                if (needLogicalDelete)
                {
                    // Delete
                    modifiedEntity.IsActive = 0;
                    modifiedEntity.UpdatedBy = updateUser;
                }
                else
                {
                    // Normal
                    modifiedEntity.IsActive = true;
                    modifiedEntity.UpdatedBy = updateUser;
                }
                modifiedEntity.UpdatedAt = now;
            }
            catch (IOException e)
            {
                // There may be no elements, so don't throw an error
                _Logger.Error(e);
            }
        }

    }
}