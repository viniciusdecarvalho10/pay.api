using System;
using System.Text.Json.Serialization;
using Pay.Api.Core.Entities;

namespace Pay.Api.Domain.Entities
{
    public abstract class BaseTenantControlAccess : BaseEntity
    {
        public Guid SubscriptionId { get; set; }
        [JsonIgnore]
        public virtual Subscription SubscriptionReferencia { get; set; }
    }
}