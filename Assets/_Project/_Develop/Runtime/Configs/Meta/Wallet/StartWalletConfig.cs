using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Configs.Meta.Waller
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int GetValueFor(CurrencyTypes currencyType)
            => _values.First(config => config.Type == currencyType).StartValue;

        public int GetAddValueFor(CurrencyTypes currencyType)
            => _values.First(config => config.Type == currencyType).AddValue;

        public int GetSpendValueFor(CurrencyTypes currencyType)
            => _values.First(config => config.Type == currencyType).SpendValue;

        public int GetResetDataValueFor(CurrencyTypes currencyType)
            => _values.First(config => config.Type == currencyType).ResetDataValue;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public int StartValue { get; private set; }
            [field: SerializeField] public int AddValue { get; private set; }
            [field: SerializeField] public int SpendValue { get; private set; }
            [field: SerializeField] public int ResetDataValue { get; private set; }
        }
    }
}