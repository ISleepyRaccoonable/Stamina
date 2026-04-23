using System;
using Assets.Project._Develop.Runtime.Configs.Meta.Waller;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class WalletValueControllerService: IDisposable
    {
        private CurrencyTypes _currencyTypes;
        private StartWalletConfig _walletConfig;
        private GameMode _gameMode;
        private WalletService _walletService;

        public WalletValueControllerService(
            ConfigsProviderService configsProviderService,
            WalletService walletService)
        {
            _walletConfig = configsProviderService.GetConfig<StartWalletConfig>();
            _walletService = walletService;
        }

        public void Initialize(GameMode gameMode, CurrencyTypes currencyTypes)
        {
            _gameMode = gameMode;
            _currencyTypes = currencyTypes;

            _gameMode.IsWon += IncreaseValue;
            _gameMode.IsDefeated += DecreaseValue;
        }

        public void Dispose()
        {
            _gameMode.IsWon -= IncreaseValue;
            _gameMode.IsDefeated -= DecreaseValue;
        }

        private void IncreaseValue()
            => _walletService.Add(_currencyTypes, _walletConfig.GetAddValueFor(_currencyTypes));


        private void DecreaseValue()
            => _walletService.Spend(_currencyTypes, _walletConfig.GetSpendValueFor(_currencyTypes));
    }
}