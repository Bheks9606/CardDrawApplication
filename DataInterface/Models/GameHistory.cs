using System;
using System.Collections.Generic;

namespace DataInterface.Models;

public partial class GameHistory
{
    public int Id { get; set; }

    public int? PlayerId { get; set; }

    public string? HandRank { get; set; }

    public string? Result { get; set; }

    public string? Cards { get; set; }

    public string? AddedBy { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? DateDeleted { get; set; }

    public string? DeletedBy { get; set; }
}
