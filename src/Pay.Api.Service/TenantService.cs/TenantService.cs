using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pay.Api.Domain.Interface.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Pay.Api.Service.TenantService
{
    public class TenantService : ITenantService
    {
        private Guid? _tenantId;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            var subscriptionId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "SubscriptionId")?.Value;
            _tenantId = !string.IsNullOrEmpty(subscriptionId) ? Guid.Parse(subscriptionId) : Guid.Empty;
        }

        public Guid? GetSubscriptionId()
        {
            return _tenantId;
        }
    }
}
