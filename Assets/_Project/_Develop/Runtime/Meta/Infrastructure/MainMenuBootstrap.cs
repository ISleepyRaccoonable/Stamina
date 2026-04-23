using System.Collections;
using Assets.Project._Develop.Runtime.Gameplay;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _conteiner;
        private GameModeSelector _gameModeSelector;
        private MetaInfoService _metaInfoService;
        private GameplayDataProvider _gameplayDataProvider;
        private PlayerDataProvider _playerDataProvider;
        private ResettingProgressService _resettingProgressService;

        public override void ProcessRegistrations(DIContainer localConteiner, IInputSceneArgs inputSceneArgs = null)
        {
            MainMenuContextRegistrations.Process(localConteiner);

            _conteiner = localConteiner;
        }

        public override IEnumerator Initiaize()
        {
            Debug.Log("Инициализация сцены меню");

            _gameplayDataProvider = _conteiner.Resolve<GameplayDataProvider>();
            _playerDataProvider = _conteiner.Resolve<PlayerDataProvider>();
            _gameModeSelector = _conteiner.Resolve<GameModeSelector>();
            _metaInfoService = _conteiner.Resolve<MetaInfoService>();
            _resettingProgressService = _conteiner.Resolve<ResettingProgressService>();

            yield return LoadOrReset(_gameplayDataProvider);
            yield return LoadOrReset(_playerDataProvider);

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");

            ICoroutinesPerformer performer = _conteiner.Resolve<ICoroutinesPerformer>();

            performer.StartPerform(_gameModeSelector.Run());

            performer.StartPerform(_metaInfoService.Run());

            performer.StartPerform(_resettingProgressService.Run());
        }

        private IEnumerator LoadOrReset<TData>(DataProvider<TData> provider) where TData : ISaveData
        {
            bool exists = false;

            yield return provider.Exist(result => exists = result);

            if (exists)
                yield return provider.Load();
            else
                provider.Reset();
        }
    }
}