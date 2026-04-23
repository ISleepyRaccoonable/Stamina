using Assets.Project._Develop.Runtime.Configs;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure {
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer conteiner, GameplayInputArgs args)
        {
            Debug.Log("Services regiatration process in gameplay scene");

            conteiner.RegisterAsSingle(c => CreateSymbolsGenerator( c, args.Configs));
            conteiner.RegisterAsSingle(c => CreateTyper( c, args.Configs));
            conteiner.RegisterAsSingle(CreateGameplayConditionsFactory);
            conteiner.RegisterAsSingle(CreateGameModeFactory);
            conteiner.RegisterAsSingle(CreateGameplayCycle);
        }

        private static SymbolsGenerator CreateSymbolsGenerator(
            DIContainer c,
            GameplayConditionsConfig gameplayConditionsConfig)
        {
            return new SymbolsGenerator(gameplayConditionsConfig);
        }

        private static Typer CreateTyper(
            DIContainer c,
            GameplayConditionsConfig gameplayConditionsConfig)
        {
            return new Typer(gameplayConditionsConfig);
        }

        private static GameplayConditionsFactory CreateGameplayConditionsFactory(DIContainer c)
        {
            Typer typer = c.Resolve<Typer>();

            return new GameplayConditionsFactory(typer);
        }

        private static GameModeFactory CreateGameModeFactory(DIContainer c)
        {
            GameplayConditionsFactory gameplayConditionsFactory = c.Resolve<GameplayConditionsFactory>();
            return new GameModeFactory(gameplayConditionsFactory);
        }

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
        {
            Typer typer = c.Resolve<Typer>();
            ICoroutinesPerformer performer = c.Resolve<ICoroutinesPerformer>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            GameModeFactory gameModeFactory = c.Resolve<GameModeFactory>();
            SymbolsGenerator symbolsGenerator = c.Resolve<SymbolsGenerator>();
            GameStatisticsService gameStatisticsService = c.Resolve<GameStatisticsService>();
            GameplayDataProvider gameplayDataProvider = c.Resolve<GameplayDataProvider>();
            PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();
            WalletValueControllerService walletValueControllerService = c.Resolve<WalletValueControllerService>();

            return new GameplayCycle(
                gameModeFactory,
                performer,
                typer,
                sceneSwitcherService,
                symbolsGenerator,
                gameStatisticsService,
                gameplayDataProvider,
                playerDataProvider,
                walletValueControllerService);
        }
    }
}