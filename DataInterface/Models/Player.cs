using System;
using System.Collections.Generic;

namespace DataInterface.Models;

public partial class Player
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? AddedBy { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? DateDeleted { get; set; }

    public string? DeletedBy { get; set; }
}
