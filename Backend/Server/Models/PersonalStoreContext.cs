using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;

namespace Server.Models;

public partial class PersonalStoreContext : DbContext
{
    public PersonalStoreContext()
    {
    }

    public PersonalStoreContext(DbContextOptions<PersonalStoreContext> options)
        : base(options)
    {
    }
    
    public DbSet<OpenIddictEntityFrameworkCoreApplication> OpenIddictApplications { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreAuthorization> OpenIddictAuthorizations { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreScope> OpenIddictScopes { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreToken> OpenIddictTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }
    
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",true,true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(true)
                .HasColumnName("isEnabled");
            entity.Property(e => e.LockDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserRole");

            entity.Property(e => e.UserId).HasMaxLength(50);
        });
        
        modelBuilder.UseOpenIddict();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
