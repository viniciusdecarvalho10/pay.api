using System;
using Pay.Api.Domain.Interface.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pay.Api.Data.Context
{
  public class RepositoryDbContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
  {
    private readonly ITenantService _tenantService;
    public RepositoryDbContextFactory() { }
    public RepositoryDbContextFactory(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }
    public RepositoryDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();

      var SQL_CONNECTION = Environment.GetEnvironmentVariable("SQL_CONNECTION") ?? @"server=localhost,1433;database=Pay;User ID=SA;Password=Pay@2022";

      optionsBuilder
          .UseSqlServer(SQL_CONNECTION);

      return new RepositoryDbContext(optionsBuilder.Options, _tenantService);
    }
  }
}
