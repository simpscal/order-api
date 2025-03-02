using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Order.Application.Common.Interfaces;
using Order.Domain.Common.Interfaces;

namespace Order.Infrastructure.Common.Services;

public class RecordCleanupHostedService(IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            unitOfWork.ProductRepository.DeleteAllSoftDeleted(TimeSpan.FromDays(30));

            await unitOfWork.CommitAsync();

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}