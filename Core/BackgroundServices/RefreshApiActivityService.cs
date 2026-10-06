using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BackgroundServices
{
    public class RefreshApiActivityService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(10);

        public RefreshApiActivityService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Render sets RENDER_EXTERNAL_URL automatically; KEEPALIVE_URL overrides it
            var baseUrl = Environment.GetEnvironmentVariable("KEEPALIVE_URL")
                ?? Environment.GetEnvironmentVariable("RENDER_EXTERNAL_URL");

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return;
            }

            var pingUrl = baseUrl.TrimEnd('/') + "/swagger/index.html";

            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var _httpClient = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();

                    await _httpClient.GetAsync(pingUrl, stoppingToken);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    Console.WriteLine($"!!! Error refreshing API activity: {ex.Message}");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
