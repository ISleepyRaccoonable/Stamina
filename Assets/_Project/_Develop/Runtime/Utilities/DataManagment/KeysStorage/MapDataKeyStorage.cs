using System;
using System.Collections.Generic;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage
{
    public class MapDataKeyStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> Keys = new Dictionary<Type, string>()
        {
            {typeof(PlayerData), "PlayerData"},
            { typeof(GameplayData), "GameplayData" },
        };

        public string GetKeyFor<TData>() where TData : ISaveData
            => Keys[typeof(TData)];
    }
}