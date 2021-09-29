using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.GamePlay.Player.Commander.CommanderModel.CLibrary
{
    [CreateAssetMenu(menuName = "Tools/CommanderLibrary", fileName = "CommanderLibrary")]
    public class CommanderLibrary : SerializedScriptableObject
    {
        public readonly Dictionary<Commanders, Scripts.CommanderModel> CommanderModels =
            new Dictionary<Commanders, Scripts.CommanderModel>();
    }

    public enum Commanders
    {
        Commander1,
        Commander2
    }
}
