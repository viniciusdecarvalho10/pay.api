using Microsoft.EntityFrameworkCore;

namespace Pay.Api.Core.Extensions
{
    public interface ISeed
    {
        void Seed(ModelBuilder modelBuilder);
    }
}