using System;
using System.Collections;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay {
    public class GameplayCycle : IDisposable
    {
        private readonly KeyCode SpaceKeyCod = KeyCode.Space;

        private GameModeFactory _gameModeFactory;
        private ICoroutinesPerformer _performer;
        private Typer _typer;
        private SceneSwitcherService _sceneSwitcherService;
        private SymbolsGenerator _symbolsGenerator;
        private GameStatisticsService _gameStatisticsService;
        private GameplayDataProvider _gameplayDataProvider;
        private PlayerDataProvider _playerDataProvider;
        private WalletValueControllerService _walletValueControllerService;

        private GameMode _gameMode;
        private string _generatedSequence;

        public GameplayCycle(
            GameModeFactory gameModeFactory,
            ICoroutinesPerformer performer,
            Typer typer,
            SceneSwitcherService sceneSwitcherService,
            SymbolsGenerator symbolsGenerator,
            GameStatisticsService gameStatisticsService,
            GameplayDataProvider gameplayDataProvider,
            PlayerDataProvider playerDataProvider,
            WalletValueControllerService walletValueControllerService)
        {
            _gameModeFactory = gameModeFactory;
            _performer = performer;
            _typer = typer;
            _sceneSwitcherService = sceneSwitcherService;
            _symbolsGenerator = symbolsGenerator;
            _gameStatisticsService = gameStatisticsService;
            _gameplayDataProvider = gameplayDataProvider;
            _playerDataProvider = playerDataProvider;
            _walletValueControllerService = walletValueControllerService;
        }

        public void Prepare()
        {
            _generatedSequence = _symbolsGenerator.Generate();

            _gameMode = _gameModeFactory.CreateGameMode(_generatedSequence);

            _gameStatisticsService.Initialize(_gameMode);

            _walletValueControllerService.Initialize(_gameMode, CurrencyTypes.Gold);
        }

        public IEnumerator Launch()
        {
            _gameMode.IsWon += OnGameModeIsWined;
            _gameMode.IsDefeated += OnGameModeIsDefeated;

            _performer.StartPerform(_typer.Start());
            _gameMode.Start();

            Debug.Log($"Сгенерированная последовательность - {_generatedSequence}");

            yield break;
        }

        public void Dispose()
        {
            _gameMode.IsWon -= OnGameModeIsWined;
            _gameMode.IsDefeated -= OnGameModeIsDefeated;
            
        }

        private void OnGameModeIsDefeated()
        {
            Debug.Log("LOOSE!");
            _performer.StartPerform(EndGameProcessForDefeat());
        }

        private void OnGameModeIsWined()
        {
            Debug.Log("WIN!");
            _performer.StartPerform(EndGameProcessForWin());
        }

        private IEnumerator EndGameProcessForWin()
        {
            Dispose();

            _gameMode.Dispose();
            _gameStatisticsService.Dispose();

            yield return _gameplayDataProvider.Save();
            yield return _playerDataProvider.Save();

            Debug.Log("Press Space to continue...");
            yield return new WaitUntil(() => Input.GetKeyDown(SpaceKeyCod));

            _performer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }

        private IEnumerator EndGameProcessForDefeat()
        {
            Dispose();

            yield return _gameplayDataProvider.Save();
            yield return _playerDataProvider.Save();

            Debug.Log("Press Space to continue...");
            yield return new WaitUntil(() => Input.GetKeyDown(SpaceKeyCod));

            Prepare();
            _performer.StartPerform(Launch());
        }
    }
}