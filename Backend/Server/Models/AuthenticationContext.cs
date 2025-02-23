using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Models;

public partial class AuthenticationContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AuthenticationContext()
    {
    }

    public AuthenticationContext(DbContextOptions<AuthenticationContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<VwEmailTemplateVerifyOtp> VwEmailTemplateVerifyOtps { get; set; }

    public virtual DbSet<VwEmailTemplateVerifyUser> VwEmailTemplateVerifyUsers { get; set; }

    public virtual DbSet<VwRole> VwRoles { get; set; }

    public virtual DbSet<VwUserLogin> VwUserLogins { get; set; }

    public virtual DbSet<VwUserVerify> VwUserVerifies { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseOpenIddict();
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<IdentityUserLogin<string>>()
            .ToTable("UserLogins")
            .HasKey(u => new { u.UserId, u.LoginProvider, u.ProviderKey });
        modelBuilder.Entity<IdentityUserRole<string>>()
            .ToTable("UserRoles")
            .HasKey(ur => new { ur.UserId, ur.RoleId });
        modelBuilder.Entity<IdentityUserToken<string>>()
            .ToTable("UserTokens")
            .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        modelBuilder.Entity<IdentityUserClaim<string>>()
            .ToTable("UserClaims")
            .HasKey(uc => uc.Id);
        modelBuilder.Entity<IdentityRoleClaim<string>>()
            .ToTable("RoleClaims")
            .HasKey(rc => rc.Id);

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__email_te__3213E83F83E83DA0");

            entity.ToTable("email_template");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreateAt)
                .HasPrecision(6)
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("create_by");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screen_name");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UpdateAt)
                .HasPrecision(6)
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("update_by");
        });
        
        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__system_c__3213E83FD2A9EC83");

            entity.ToTable("system_config");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(100)
                .HasColumnName("screen_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<VwEmailTemplateVerifyOtp>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EmailTemplate_VerifyOTP");

            entity.Property(e => e.EmailBody).HasColumnName("email_body");
            entity.Property(e => e.EmailTitle).HasColumnName("email_title");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screen_name");
        });

        modelBuilder.Entity<VwEmailTemplateVerifyUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EmailTemplate_VerifyUser");

            entity.Property(e => e.EmailBody).HasColumnName("email_body");
            entity.Property(e => e.EmailTitle).HasColumnName("email_title");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screen_name");
        });

        modelBuilder.Entity<VwRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Role");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<VwUserLogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserLogin");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.RoleName).HasMaxLength(256);
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<VwUserVerify>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserVerify");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
