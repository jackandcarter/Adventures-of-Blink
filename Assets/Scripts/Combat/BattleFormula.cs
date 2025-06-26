using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.Combat
{
    /// <summary>
    /// Static helper containing the game's damage and defense calculations.
    /// Adjust constants here or expose them via the editor.
    /// </summary>
    public static class BattleFormula
    {
        public static float CalculateDamage(RuntimeStats attacker, RuntimeStats defender, AbilityData ability)
        {
            float attackPower = attacker.Attack + ability.baseDamage;
            float damage = attackPower - defender.Defense;
            return UnityEngine.Mathf.Max(1f, damage);
        }
    }
}
