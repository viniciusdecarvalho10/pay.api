using System;

namespace Pay.Api.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public virtual bool? Deleted { get; set; }

        public virtual DateTime? DeleteDate { get; set; }

        public virtual DateTime? UpdateDate { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Guid? DeleteUserId { get; set; }

        public virtual Guid? UpdateUserId { get; set; }

        public virtual Guid? CreateUserId { get; set; }
    }
}