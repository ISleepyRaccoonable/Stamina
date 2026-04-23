using System;
using Assets.Project._Develop.Runtime.Utilities.DataManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class GameStatisticsService : IDataReader<GameplayData>, IDataWriter<GameplayData>, IDisposable
    {
        private GameMode _gameMode;

        public int WinsCount { get; private set; }
        public int DefeatsCount { get; private set; }

        public GameStatisticsService(
            GameplayDataProvider gameplayDataProvider)
        {
            gameplayDataProvider.RegisterReader(this);
            gameplayDataProvider.RegisterWriter(this);
        }

        public void Initialize(GameMode gameMode)
        {
            _gameMode = gameMode;

            _gameMode.IsDefeated += IncreaseDefeats;
            _gameMode.IsWon += IncreaseWins;
        }

        public void Dispose()
        {
            _gameMode.IsDefeated -= IncreaseDefeats;
            _gameMode.IsWon -= IncreaseWins;
        }

        public void Reset()
        {
            WinsCount = 0;
            DefeatsCount = 0;
        }

        private void IncreaseWins() => WinsCount++;
        private void IncreaseDefeats() => DefeatsCount++;

        public void ReadFrom(GameplayData data)
        {
            WinsCount = data.WinsCount;
            DefeatsCount = data.DefeatsCount;
        }

        public void WriteTo(GameplayData data)
        {
            if (data == null)
                throw new NullReferenceException($"{nameof(data)} is null!");

            data.DefeatsCount = DefeatsCount;
            data.WinsCount = WinsCount;
        }
    }
}