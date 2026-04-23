using System.Text;
using Assets.Project._Develop.Runtime.Configs;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class SymbolsGenerator
    {
        private GameplayConditionsConfig _config;

        public SymbolsGenerator(GameplayConditionsConfig config) => _config = config;

        public string Generate()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < _config.CountOfSymbolsInSequence; i++)
            {
                var randomIndex = Random.Range(0, _config.SymbolsUsed.Length);
                var randomCharacter = _config.SymbolsUsed[randomIndex];
                stringBuilder.Append(randomCharacter);
            }

            return stringBuilder.ToString();
        }
    }
}