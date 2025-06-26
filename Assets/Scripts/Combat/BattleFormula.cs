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
            if (attacker == null || defender == null || ability == null)
                return 0f;

            float attack = UnityEngine.Mathf.Max(1f, attacker.Attack);
            float defense = UnityEngine.Mathf.Max(1f, defender.Defense);

            // Base scaling based on attacker's power and defender's toughness
            float scaledDamage = (attack / defense) * ability.baseDamage;

            // Apply ±10% random jitter to prevent perfectly deterministic combat
            float jitter = UnityEngine.Random.Range(0.9f, 1.1f);

            float damage = scaledDamage * jitter;
            return UnityEngine.Mathf.Max(1f, damage);
        }
    }
}
