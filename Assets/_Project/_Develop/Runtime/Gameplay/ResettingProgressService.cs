using System.Collections;
using Assets.Project._Develop.Runtime.Configs.Meta.Waller;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class ResettingProgressService
    {
        private const KeyCode KeyCodeAlpha4 = KeyCode.Alpha4;

        private PlayerDataProvider _playerDataProvider;
        private GameplayDataProvider _gameplayDataProvider;
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public ResettingProgressService(
            PlayerDataProvider playerDataProvider,
            GameplayDataProvider gameplayDataProvider,
            WalletService walletService,
            ConfigsProviderService configsProviderService)
        {
            _playerDataProvider = playerDataProvider;
            _gameplayDataProvider = gameplayDataProvider;
            _walletService = walletService;
            _configsProviderService = configsProviderService;
        }

        public IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.anyKeyDown);

                if (Input.GetKeyDown(KeyCodeAlpha4))
                {
                    CurrencyTypes currencyType = CurrencyTypes.Gold;

                    StartWalletConfig walletConfig = _configsProviderService.GetConfig<StartWalletConfig>();

                    var resetValue = walletConfig.GetResetDataValueFor(currencyType);

                    Debug.Log($"{_walletService.GetCurrency(currencyType).Value} >= {resetValue}");

                    if (_walletService.GetCurrency(currencyType).Value >= resetValue)
                    {
                        Debug.Log("Data was reseted!");

                        _playerDataProvider.Reset();
                        _gameplayDataProvider.Reset();

                        yield return _playerDataProvider.Save();
                        yield return _gameplayDataProvider.Save();
                    }
                    else
                    {
                        Debug.Log("Not enough money for reset!");
                    }
                }
            }
        }
    }
}