using Microsoft.EntityFrameworkCore;
using Pay.Api.Data.Migrations;

namespace Pay.Api.Data.Extensions
{
    public static partial class ModelBuilderExtensions
    {
        public static void Registrations(this ModelBuilder modelBuilder)
        {
            //#Add new Registrations#
            RegisterSeed<SubscriptionSeed>();
        }
    }
}
