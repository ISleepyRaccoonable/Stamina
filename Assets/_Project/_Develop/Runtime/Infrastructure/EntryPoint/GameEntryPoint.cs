using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Infrastructure.LoadingScreen;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectConteiner = new DIContainer();

            ProjectContextRegistrations.Process(projectConteiner);

            projectConteiner.Initialize();

            projectConteiner.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(projectConteiner));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60; 
        }

        private IEnumerator Initialize(DIContainer conteiner)
        {
            ILoadingScreen loadingScreen = conteiner.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = conteiner.Resolve<SceneSwitcherService>();

            loadingScreen.Show();

            Debug.Log("Начинается инициализация сервисов");

            yield return conteiner.Resolve<ConfigsProviderService>().LoadAsync();

            Debug.Log("Завершается инициализация сервисов");

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}