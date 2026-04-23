using UnityEngine;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using Assets.Project._Develop.Runtime.Gameplay;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets.Project._Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer conteiner)
        {
            Debug.Log("Services regiatration process in Main Menu scene");

            conteiner.RegisterAsSingle(CreateGameModeSelector);

            conteiner.RegisterAsSingle(CreateMetaInfoService);

            conteiner.RegisterAsSingle(CreateResettingProgressService);
        }

        private static GameModeSelector CreateGameModeSelector(DIContainer c)
        {
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();

            return new GameModeSelector(
                configsProviderService,
                coroutinesPerformer,
                sceneSwitcherService);
        }

        private static MetaInfoService CreateMetaInfoService(DIContainer c)
        {
            GameStatisticsService gameStatisticsService = c.Resolve<GameStatisticsService>();
            WalletService walletService = c.Resolve<WalletService>();

            return new MetaInfoService(gameStatisticsService, walletService);
        }

        private static ResettingProgressService CreateResettingProgressService(DIContainer c)
        {
            return new ResettingProgressService(
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<GameplayDataProvider>(),
                c.Resolve<WalletService>(),
                c.Resolve<ConfigsProviderService>()
                );
        }
    }
}
