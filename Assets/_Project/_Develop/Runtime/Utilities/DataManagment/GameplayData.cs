using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment
{
    [System.Serializable]
    public class GameplayData : ISaveData
    {
        public int WinsCount;
        public int DefeatsCount;
    }
}