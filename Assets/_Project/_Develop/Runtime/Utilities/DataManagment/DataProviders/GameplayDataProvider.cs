namespace Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public class GameplayDataProvider : DataProvider<GameplayData>
    {
        public GameplayDataProvider(ISaveLoadService saveLoadService) : base(saveLoadService)
        {

        }

        protected override GameplayData GetOriginData()
        {
            return new GameplayData
            {
                WinsCount = 0,
                DefeatsCount = 0,
            };
        }
    }
}