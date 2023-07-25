using SysCredit.Api.Helpers.Atributtes;

namespace SysCredit.Api.ViewModels.Reference;

public class ReferenceOption
{
    [Sortable]
    [Searchable]
    public long RelationshipId { get; set; }
    [Sortable]
    [Searchable]
    public string Name { get; set; } = string.Empty;
}
