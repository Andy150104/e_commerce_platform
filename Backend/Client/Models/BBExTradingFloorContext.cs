using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace server.Models;

public partial class BBExTradingFloorContext : DbContext
{
    public BBExTradingFloorContext()
    {
    }

    public BBExTradingFloorContext(DbContextOptions<BBExTradingFloorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Exchange> Exchanges { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrdersExchange> OrdersExchanges { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Queue> Queues { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<VwUserAuthentication> VwUserAuthentications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__address__CAA247C8432913D5");

            entity.ToTable("address");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("address_id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .HasColumnName("address_line");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .HasColumnName("district");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Ward)
                .HasMaxLength(50)
                .HasColumnName("ward");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__address__user_id__3D5E1FD2");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__carts__2EF52A27293D50AF");

            entity.ToTable("carts");

            entity.Property(e => e.CartId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("cart_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__carts__user_id__60A75C0F");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__cart_ite__5D9A6C6E40B635F8");

            entity.ToTable("cart_items");

            entity.Property(e => e.CartItemId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("cart_item_id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__cart_item__cart___656C112C");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__cart_item__produ__66603565");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B446C64931");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__categorie__paren__44FF419A");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("email_template");

            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreateAt)
                .HasPrecision(6)
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("create_by");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
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

        modelBuilder.Entity<Exchange>(entity =>
        {
            entity.HasKey(e => e.ExchangeId).HasName("PK__exchange__FAAC5D3E4493B1F6");

            entity.ToTable("exchanges");

            entity.Property(e => e.ExchangeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("exchange_id");
            entity.Property(e => e.OrderExchangeId).HasColumnName("order_exchange_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Product).WithMany(p => p.Exchanges)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__exchanges__produ__6A30C649");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messages__0BBF6EE6AC5B298C");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("message_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK__messages__receiv__778AC167");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__messages__sender__76969D2E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__465962296F3DB48B");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__orders__user_id__571DF1D5");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__order_de__3C5A408059ED4BF8");

            entity.ToTable("order_details");

            entity.Property(e => e.OrderDetailId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("order_detail_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_det__order__5BE2A6F2");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_det__produ__5CD6CB2B");
        });

        modelBuilder.Entity<OrdersExchange>(entity =>
        {
            entity.HasKey(e => e.OrderExchangeId).HasName("PK__orders_e__46B9A428DEC80F3E");

            entity.ToTable("orders_exchanges");

            entity.Property(e => e.OrderExchangeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("order_exchange_id");
            entity.Property(e => e.ExchangeId).HasColumnName("exchange_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Exchange).WithMany(p => p.OrdersExchanges)
                .HasForeignKey(d => d.ExchangeId)
                .HasConstraintName("FK__orders_ex__excha__6E01572D");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5F00DBA61");

            entity.ToTable("products");

            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.Products)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__products__user_i__4BAC3F29");
        });

        modelBuilder.Entity<Queue>(entity =>
        {
            entity.HasKey(e => e.QueueId).HasName("PK__queue__2294FA6E95CC6F43");

            entity.ToTable("queue");

            entity.Property(e => e.QueueId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("queue_id");
            entity.Property(e => e.ExchangeId).HasColumnName("exchange_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Exchange).WithMany(p => p.Queues)
                .HasForeignKey(d => d.ExchangeId)
                .HasConstraintName("FK__queue__exchange___72C60C4A");

            entity.HasOne(d => d.Product).WithMany(p => p.Queues)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__queue__product_i__71D1E811");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__reports__779B7C58F9AEABDF");

            entity.ToTable("reports");

            entity.Property(e => e.ReportId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("report_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__reports__user_id__7C4F7684");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__reviews__60883D90B8CBFA35");

            entity.ToTable("reviews");

            entity.Property(e => e.ReviewId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("review_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReplyReviewContent).HasColumnName("reply_review_content");
            entity.Property(e => e.ReviewContent).HasColumnName("review_content");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__reviews__product__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__reviews__user_id__4F7CD00D");
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F90DE47C7");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserName, "UQ__users__66DCF95C2B543528").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164C7381CD8").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("user_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__vouchers__80B6FFA8329DC229");

            entity.ToTable("vouchers");

            entity.Property(e => e.VoucherId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("voucher_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__vouchers__order___02FC7413");

            entity.HasOne(d => d.Product).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__vouchers__produc__02084FDA");
        });

        modelBuilder.Entity<VwUserAuthentication>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserAuthentication");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
