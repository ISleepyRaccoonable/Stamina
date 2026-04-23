using UnityEngine;
using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using System;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayCycle _gameplayCycle;

        public override void ProcessRegistrations(DIContainer projectConteiner, IInputSceneArgs inputSceneArgs = null)
        {
            _container = new DIContainer(projectConteiner);

            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(inputSceneArgs)} is not MatchTargetWeightMask with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initiaize()
        {
            Debug.Log("Инициализация геймплейной сцены");

            _gameplayCycle = _container.Resolve<GameplayCycle>();

            _gameplayCycle.Prepare();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");

            _container.Resolve<ICoroutinesPerformer>().StartPerform(_gameplayCycle.Launch());
        }

        private void OnDisable()
        {
            _gameplayCycle.Dispose();
        }
    }
}