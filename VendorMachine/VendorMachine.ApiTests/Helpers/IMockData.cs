using Microsoft.Extensions.DependencyInjection;
using VendorMachine.Core.Data;

namespace VendorMachine.ApiTests.Helpers
{
    public interface IMockData
    {
        ApiDbContext GetContextWithData(ServiceProvider _serviceProvider);
    }
}