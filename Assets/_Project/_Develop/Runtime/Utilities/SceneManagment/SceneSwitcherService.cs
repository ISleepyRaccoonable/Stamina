using System;
using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Infrastructure.LoadingScreen;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Project._Develop.Runtime.Utilities.SceneManagment
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;
        private readonly DIContainer _projectConteiner;

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService,
            ILoadingScreen loadingScreen,
            DIContainer projectConteiner)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _projectConteiner = projectConteiner;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs inputSceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            yield return new WaitForSeconds(.5f);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException($"{typeof(SceneBootstrap)} is null");

            DIContainer sceneContainer = new DIContainer(_projectConteiner);

            sceneBootstrap.ProcessRegistrations(sceneContainer, inputSceneArgs);

            sceneContainer.Initialize();

            yield return sceneBootstrap.Initiaize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}