using Pay.Api.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Pay.Api.Core.Extensions
{
    public static class ClaimsIdentityExtensions
    {

        public static PayIdentity GetIdentity(this ClaimsIdentity identity, ILogger logger = null) => identity.Claims.GetIdentity(logger);

        public static PayIdentity GetIdentity(this IEnumerable<Claim> claims, ILogger logger = null)
        {
            if (claims == null || claims.Count() == 0)
                return new PayIdentity();

            var PayIdentity = new PayIdentity();

            try
            {
                var idSubscription = Guid.Parse(claims.FirstOrDefault(c => c.Type == "SubscriptionId").Value.ToString());
                PayIdentity.SubscriptionId = idSubscription;
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Error to load PayIdentity SubscriptionId");
            }

            try
            {
                var userId = Guid.Parse(claims.FirstOrDefault(c => c.Type == "UserId").Value.ToString());
                PayIdentity.UserId = userId;
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Error to load PayIdentity UserId");
            }

            try
            {
                var userEmail = claims.FirstOrDefault(c => c.Type == "UserEmail").Value;
                PayIdentity.UserEmail = userEmail;
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Error to load PayIdentity UserEmail");
            }

            return PayIdentity;
        }

    }
}