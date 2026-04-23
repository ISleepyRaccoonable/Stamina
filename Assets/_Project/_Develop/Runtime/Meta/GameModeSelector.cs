using System.Collections;
using Assets.Project._Develop.Runtime.Configs;
using Assets.Project._Develop.Runtime.Gameplay.Infrastructure;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Meta
{
    public class GameModeSelector
    {
        private const KeyCode KeyCodeAlpha1 = KeyCode.Alpha1;
        private const KeyCode KeyCodeAlpha2 = KeyCode.Alpha2;

        private GameplayConditionsConfig _gameplayConditionsConfig;
        private ConfigsProviderService _configsProviderService;
        private ICoroutinesPerformer _performer;
        private SceneSwitcherService _sceneSwitcherService;

        public GameModeSelector(
            ConfigsProviderService configsProviderService,
            ICoroutinesPerformer performer,
            SceneSwitcherService sceneSwitcherService)
        {
            _configsProviderService = configsProviderService;
            _performer = performer;
            _sceneSwitcherService = sceneSwitcherService;
        }

        public IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.anyKeyDown);

                if (Input.GetKeyDown(KeyCodeAlpha1))
                {
                    _gameplayConditionsConfig = _configsProviderService.GetConfigBy<GameplayConditionsConfig>(ConfigsName.Digits);

                    _performer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(
                        Scenes.Gameplay,
                        new GameplayInputArgs(_gameplayConditionsConfig)));

                    yield break;
                }

                if (Input.GetKeyDown(KeyCodeAlpha2))
                {
                    _gameplayConditionsConfig = _configsProviderService.GetConfigBy<GameplayConditionsConfig>(ConfigsName.Letters);

                    _performer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(
                        Scenes.Gameplay,
                        new GameplayInputArgs(_gameplayConditionsConfig)));

                    yield break;
                }
            }
        }
    }
}