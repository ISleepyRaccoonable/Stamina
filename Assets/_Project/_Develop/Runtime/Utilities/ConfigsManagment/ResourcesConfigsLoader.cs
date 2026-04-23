using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Project._Develop.Runtime.Configs;
using Assets.Project._Develop.Runtime.Configs.Meta.Waller;
using Assets.Project._Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPaths = new()
        {
            { typeof(GameplayConditionsConfig), "Configs/GameplayConditionsConfigs" },
            { typeof(StartWalletConfig), "Configs/Meta/Wallet/StartWalletConfig" },
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configResourcesPath in _configsResourcesPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
                loadedConfigs.Add(configResourcesPath.Key, config);
                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }

        public IEnumerator LoadMultiAsync(Action<Dictionary<Type, Dictionary<string, object>>> onLoaded)
        {
            var result = new Dictionary<Type, Dictionary<string, object>>();

            foreach (var pair in _configsResourcesPaths)
            {
                var configs = _resources.LoadAll<ScriptableObject>(pair.Value);

                foreach (var config in configs)
                {
                    var type = config.GetType();

                    if (result.ContainsKey(type) == false)
                        result[type] = new Dictionary<string, object>();

                    result[type].Add(config.name, config);
                }

                yield return null;
            }

            onLoaded?.Invoke(result);
        }
    }
}