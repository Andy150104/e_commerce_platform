using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class User
{
    public string UserName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? ImageUrl { get; set; }

    public DateOnly? BirthDate { get; set; }

    public byte? Gender { get; set; }

    public Guid? PlanId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? PlanExpired { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<BlindBox> BlindBoxes { get; set; } = new List<BlindBox>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<OrderPlan> OrderPlans { get; set; } = new List<OrderPlan>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Plan? Plan { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
