using System;
using SQLite;

namespace Ledger.App.Models;

[Table("Ledgers")]
public class LedgerModel
{
    [PrimaryKey, AutoIncrement, Unique]
    public int LedgerId { get; set; }
    [NotNull, Indexed]
    public int ProjectId { get; set; }
    [Indexed, NotNull]
    public int TagId { get; set; }
    [NotNull]
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    [NotNull]
    public string StartDate { get; set; }
    [NotNull]
    public string StartTime { get; set; }
    [NotNull]
    public string EndDate { get; set; }
    [NotNull]
    public string EndTime { get; set; }
}