using System;
using Pay.Api.Core.Entities;

namespace Pay.Api.Domain.Entities
{
    public class UserSubscription : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User UserReferencia { get; set; }
        public Guid SubscriptionId { get; set; }
        public virtual Subscription SubscriptionReferencia { get; set; }
    }
}