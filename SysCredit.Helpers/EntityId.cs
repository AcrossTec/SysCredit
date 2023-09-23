namespace SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
public class EntityId
{
    /// <summary>
    /// 
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="EntityId"></param>
    public static implicit operator long?(EntityId EntityId) => EntityId.Id;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    public static implicit operator EntityId(long? Id) => new EntityId { Id = Id };
}
