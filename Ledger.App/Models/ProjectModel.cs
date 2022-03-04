using SQLite;

namespace Ledger.App.Models;

[Table("Projects")]
public class ProjectModel
{
    [PrimaryKey, Unique]
    public int ProjectId { get; set; }
    [NotNull, Unique]
    public string ProjectName { get; set; }
}