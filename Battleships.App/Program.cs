using Battleships.Data.Objects;
using Battleships.Logic;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Battleships.App
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            RegisterComponents();
            var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<BattleshipGame>().Run();
            DisposeServiceProvider();
        }

        private static void RegisterComponents()
        {
            var services = new ServiceCollection();
            services.AddScoped<BattleshipGame>();
            // var gameBoard = new GameBoard();
            services.AddSingleton<GameBoard>();
            services.AddScoped<GameStrategyCreator>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServiceProvider()
        {
            if (_serviceProvider == null)
                return;

            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
