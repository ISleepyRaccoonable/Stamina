namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class GameModeFactory
    {
        private GameplayConditionsFactory _gameplayConditionsFactory;

        public GameModeFactory(GameplayConditionsFactory gameplayConditionsFactory)
        {
            _gameplayConditionsFactory = gameplayConditionsFactory;
        }

        public GameMode CreateGameMode(string generatedSequence)
        {
            return new GameMode(
                _gameplayConditionsFactory.CreateWinCondition(generatedSequence),
                _gameplayConditionsFactory.CreateDefeatCondition(generatedSequence));
        }
    }
}