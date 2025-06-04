using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.Combat
{
    /// <summary>
    /// Static helper containing the game's damage and defense calculations.
    /// Adjust constants here or expose them via the editor.
    /// </summary>
    public static class BattleFormula
    {
        public static float CalculateDamage(CharacterStats attacker, CharacterStats defender, AbilityData ability)
        {
            float attackPower = attacker.attack + ability.baseDamage;
            float damage = attackPower - defender.defense;
            return UnityEngine.Mathf.Max(1f, damage);
        }
    }
}
