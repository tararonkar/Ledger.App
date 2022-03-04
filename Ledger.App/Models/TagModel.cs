using SQLite;

namespace Ledger.App.Models;
[Table("Tags")]
public class TagModel
{
    [PrimaryKey, Unique]
    public int TagId { get; set; }
    [NotNull]
    public string Tag { get; set; }
}