using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistrations(DIContainer container, IInputSceneArgs inputSceneArgs = null);

        public abstract IEnumerator Initiaize();

        public abstract void Run();
    }
}