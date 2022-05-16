using Microsoft.Extensions.DependencyInjection;

namespace Pay.Api.Domain.Interface
{
    public interface IDependencyGroup
    {
        void Register(IServiceCollection serviceCollection);
    }
}