using Pay.Api.Domain.Interface.Services;
using Microsoft.EntityFrameworkCore;

namespace Pay.Api.Data.Context
{
    public partial class RepositoryDbContext : DbContext
    {
        private readonly ITenantService _tenantService;
        public RepositoryDbContext(DbContextOptions options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
        }

        public void RunMigrate()
        {
            this.Database.Migrate();
        }

        public void Drop()
        {
            this.Database.EnsureDeleted();
        }
    }
}
