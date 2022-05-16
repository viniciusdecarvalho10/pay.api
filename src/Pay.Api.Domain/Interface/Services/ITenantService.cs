using System;
using System.Collections.Generic;

namespace Pay.Api.Domain.Interface.Services
{
    public interface ITenantService
    {
        Guid? GetSubscriptionId();
    }
}
