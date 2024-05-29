namespace BigonWebUI.Models.Entities.Common
{
    public abstract class BaseEntity<Tkey> : AuditableEntity
        where Tkey : struct
    {
        public Tkey Id { get; set; }
    }
}
