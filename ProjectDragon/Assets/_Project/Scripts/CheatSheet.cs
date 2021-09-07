using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "New CheatSheet", menuName = "CheatSheets")]
    public class CheatSheet : ScriptableObject
    {
        private List<Cheat> _usableCheats;

        private Dictionary<string, Cheat> _cheats;

        private void OnValidate()
        {
            _cheats = new Dictionary<string, Cheat>();

            foreach (Cheat cheat in _usableCheats)
            {
                _cheats.Add(cheat.Command, cheat);
            }
        }
    }
}
