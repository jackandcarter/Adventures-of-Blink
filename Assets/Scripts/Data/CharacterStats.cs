using UnityEngine;

namespace AdventuresOfBlink.Data
{
    /// <summary>
    /// Base stats for a character or enemy.
    /// These can be extended with equipment or buffs.
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "AdventuresOfBlink/Character Stats", order = 2)]
    public class CharacterStats : ScriptableObject
    {
        public int maxHealth = 100;
        public int attack = 10;
        public int defense = 5;
        public int speed = 5;
    }
}
