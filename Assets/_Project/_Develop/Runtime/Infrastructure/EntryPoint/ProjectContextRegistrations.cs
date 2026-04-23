using System;
using System.Collections.Generic;
using Assets.Project._Develop.Runtime.Gameplay;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Infrastructure.LoadingScreen;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using Assets.Project._Develop.Runtime.Utilities;
using Assets.Project._Develop.Runtime.Utilities.AssetsManagment;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.Serializers;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        private readonly static string _extension = "json";

        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);

            container.RegisterAsSingle(CreateResourcesAssetsLoader);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle<ILoadingScreen>(CreateStandartLoadingScreen);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);

            container.RegisterAsSingle(CreatePlayerDataProvider);

            container.RegisterAsSingle(CreateGameplayDataProvider);

            container.RegisterAsSingle(CreateWalletService).NonLazy();

            container.RegisterAsSingle(CreateGameStatisticsService).NonLazy();

            container.RegisterAsSingle(CreateWalletValueControllerService);
        }

        private static WalletValueControllerService CreateWalletValueControllerService (DIContainer c)
        {
            return new WalletValueControllerService(
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<WalletService>()
                );
        }

        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
            => new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());

        private static GameplayDataProvider CreateGameplayDataProvider(DIContainer c)
            => new GameplayDataProvider(c.Resolve<ISaveLoadService>());

        private static GameStatisticsService CreateGameStatisticsService(DIContainer c)
        {
            return new GameStatisticsService(c.Resolve<GameplayDataProvider>());
        }

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeysStorage dataKeysStorage = new MapDataKeyStorage();

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataRepository dataRepository = new LocalDataFileRepository(saveFolderPath, _extension);

            return new SaveLoadService(dataSerializer, dataKeysStorage, dataRepository);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c) => new SceneLoaderService();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return GameObject.Instantiate(coroutinesPerformerPrefab);
        }

        private static StandartLoadingScreen CreateStandartLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandartLoadingScreen standartLoadingScreenPrefab = resourcesAssetsLoader
                .Load<StandartLoadingScreen>("Utilities/StandardLoadingScene");

            return GameObject.Instantiate(standartLoadingScreenPrefab);
        }
    }
}