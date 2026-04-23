using Assets.Project._Develop.Runtime.Gameplay;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/GameplayConditionsConfig", fileName = "GameplayConditionsConfig")]
    public class GameplayConditionsConfig : ScriptableObject
    {
        [field: SerializeField] public SymbolsType SymbolsType { get; private set; }

        [field: SerializeField] public int CountOfSymbolsInSequence { get; private set; }
        [field: SerializeField] public string SymbolsUsed { get; private set; }
    }
}