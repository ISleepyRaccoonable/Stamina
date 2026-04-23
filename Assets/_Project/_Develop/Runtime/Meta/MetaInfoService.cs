using System.Collections;
using Assets.Project._Develop.Runtime.Gameplay;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Meta
{
    public class MetaInfoService
    {
        private const KeyCode KeyCodeAlpha3 = KeyCode.Alpha3;

        private GameStatisticsService _gameStatisticsService;
        private WalletService _walletService;

        public MetaInfoService(
            GameStatisticsService gameStatisticsService,
            WalletService walletService)
        {
            _gameStatisticsService = gameStatisticsService;
            _walletService = walletService;
        }

        public IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.anyKeyDown);

                if (Input.GetKeyDown(KeyCodeAlpha3))
                {
                    Debug.Log($"Побед: {_gameStatisticsService.WinsCount}");
                    Debug.Log($"Поражений: {_gameStatisticsService.DefeatsCount}");
                    Debug.Log($"Золото: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
                }
            }
        }

    }
}