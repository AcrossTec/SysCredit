namespace SysCredit.Api.Helpers
{
    public class EntityId
    {
        public long? Id { get; set; }

        public static implicit operator long?(EntityId EntityId) => EntityId.Id;

        public static implicit operator EntityId(long? Id) => new EntityId { Id = Id };
    }
}
