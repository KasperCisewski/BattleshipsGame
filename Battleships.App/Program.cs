using Battleships.App.Data;
using Battleships.Data.Objects;
using Battleships.Logic;
using Battleships.Logic.Services;
using Battleships.Logic.Services.Implementation;
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
            services.AddSingleton(new GameBoard(AppData.BoardData.BoardSize, AppData.ShipData.ShipsWithQuantity));
            services.AddScoped<GameStrategyCreator>();
         //   services.AddScoped<PlayerBoardBuilder>();
            services.AddScoped<IBoardService, BoardService>();
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
