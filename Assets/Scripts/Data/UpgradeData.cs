using UnityEngine;

namespace AdventuresOfBlink.Data
{
    /// <summary>
    /// Describes an upgrade that unlocks abilities or boosts stats.
    /// </summary>
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "AdventuresOfBlink/Upgrade", order = 5)]
    public class UpgradeData : ScriptableObject
    {
        [Header("Info")]
        public string upgradeName;
        public Sprite icon;
        [TextArea]
        public string description;

        [Header("Requirements")]
        public Requirement[] requirements;

        [Header("Rewards")]
        [Tooltip("Ability granted when the upgrade is applied.")]
        public AbilityData abilityUnlock;
        [Tooltip("Index of Duke ability to upgrade. -1 means none.")]
        public int dukeAbilityIndex = -1;
        [Tooltip("Stat boosts applied to Blink after upgrading.")]
        public StatBoost statBoost;

        [System.Serializable]
        public class Requirement
        {
            public ItemData item;
            public int amount = 1;
        }

        [System.Serializable]
        public class StatBoost
        {
            public int health;
            public int attack;
            public int defense;
            public int speed;
            public int logic;
            public int energy;
        }
    }
}
