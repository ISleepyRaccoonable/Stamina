using Assets.Project._Develop.Runtime.Configs;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameplayConditionsConfig configs)
        {
            Configs = configs;
        }

        public GameplayConditionsConfig Configs { get; }
    }
}