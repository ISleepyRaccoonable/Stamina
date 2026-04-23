using System;
using System.Collections;
using System.Text;
using Assets.Project._Develop.Runtime.Configs;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class Typer
    {
        public event Action<string> IsTyped;

        private GameplayConditionsConfig _gameplayConditionsConfig;

        public Typer(GameplayConditionsConfig gameplayConditionsConfig)
        {
            _gameplayConditionsConfig = gameplayConditionsConfig;
        }

        public IEnumerator Start()
        {
            int typedCountOfSymbols = 0;
            StringBuilder stringBuilder = new StringBuilder();

            while (typedCountOfSymbols < _gameplayConditionsConfig.CountOfSymbolsInSequence)
            {
                yield return new WaitUntil(() => Input.anyKeyDown);

                string input = Input.inputString;

                if (string.IsNullOrEmpty(input) == false)
                {
                    foreach (char c in input)
                    {
                        if (c == ' ')
                            continue;

                        stringBuilder.Append(c);
                        typedCountOfSymbols++;

                        Debug.Log($"|{stringBuilder}|");

                        if (typedCountOfSymbols >= _gameplayConditionsConfig.CountOfSymbolsInSequence)
                            break;
                    }
                }

                yield return null;
            }

            IsTyped?.Invoke(stringBuilder.ToString());
        }
    }
}