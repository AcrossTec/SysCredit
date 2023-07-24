namespace SysCredit.Api.Models;

public abstract record class Entity
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsEdit { get; set; }

    public bool IsDelete { get; set; }
}
