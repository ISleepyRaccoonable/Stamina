using System.Collections.Generic;
using Assets.Project._Develop.Runtime.Meta.Features.Wallet;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment
{
    [System.Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}