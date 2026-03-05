using System;
using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIConteiner _conteiner;
        private GameModeSelector _gameModeSelector;

        public override void ProcessRegistrations(DIConteiner localConteiner, IInputSceneArgs inputSceneArgs = null)
        {
            MainMenuContextRegistrations.Process(localConteiner);

            _conteiner = localConteiner;
        }

        public override IEnumerator Initiaize()
        {
            Debug.Log("Инициализация сцены меню");

            _gameModeSelector = _conteiner.Resolve<GameModeSelector>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");

            _conteiner.Resolve<ICoroutinesPerformer>().StartPerform(_gameModeSelector.Run());
        }
    }
}