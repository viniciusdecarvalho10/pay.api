using System;

namespace Pay.Api.Core.Entities
{
    public interface IPayIdentity
    {
        Guid SubscriptionId { get; set; }
        Guid UserId { get; set; }
        string UserEmail { get; set; }
    }
}
