using UnityEngine;

namespace AdventuresOfBlink.Data
{
    /// <summary>
    /// Runtime copy of <see cref="CharacterStats"/> with additive and
    /// percentage modifiers. Values are cloned from the source asset
    /// when constructed so that scriptable objects remain unchanged.
    /// </summary>
    [System.Serializable]
    public class RuntimeStats
    {
        public int baseMaxHealth;
        public int baseAttack;
        public int baseDefense;
        public int baseSpeed;
        public int baseLogic;
        public int baseEnergy;

        [Header("Additive Modifiers")]
        public int bonusMaxHealth;
        public int bonusAttack;
        public int bonusDefense;
        public int bonusSpeed;
        public int bonusLogic;
        public int bonusEnergy;

        [Header("Percentage Modifiers")]
        public float percentMaxHealth;
        public float percentAttack;
        public float percentDefense;
        public float percentSpeed;
        public float percentLogic;
        public float percentEnergy;

        public RuntimeStats() { }

        public RuntimeStats(CharacterStats source)
        {
            CopyFrom(source);
        }

        /// <summary>
        /// Copies base values from a CharacterStats asset.
        /// </summary>
        public void CopyFrom(CharacterStats source)
        {
            if (source == null)
                return;
            baseMaxHealth = source.maxHealth;
            baseAttack = source.attack;
            baseDefense = source.defense;
            baseSpeed = source.speed;
            baseLogic = source.logic;
            baseEnergy = source.energy;
        }

        public int MaxHealth => Mathf.RoundToInt((baseMaxHealth + bonusMaxHealth) * (1f + percentMaxHealth));
        public int Attack => Mathf.RoundToInt((baseAttack + bonusAttack) * (1f + percentAttack));
        public int Defense => Mathf.RoundToInt((baseDefense + bonusDefense) * (1f + percentDefense));
        public int Speed => Mathf.RoundToInt((baseSpeed + bonusSpeed) * (1f + percentSpeed));
        public int Logic => Mathf.RoundToInt((baseLogic + bonusLogic) * (1f + percentLogic));
        public int Energy => Mathf.RoundToInt((baseEnergy + bonusEnergy) * (1f + percentEnergy));
    }
}
