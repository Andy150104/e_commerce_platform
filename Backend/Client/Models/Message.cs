using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Message
{
    public Guid MessageId { get; set; }

    public Guid? SenderId { get; set; }

    public Guid? ReceiverId { get; set; }

    public string? Content { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual User? Receiver { get; set; }

    public virtual User? Sender { get; set; }
}
