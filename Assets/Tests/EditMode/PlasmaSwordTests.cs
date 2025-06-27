using NUnit.Framework;
using UnityEngine;
using AdventuresOfBlink.Combat;
using AdventuresOfBlink.Data;

public class PlasmaSwordTests
{
    [Test]
    public void PerformAttack_ReducesTargetHealth()
    {
        // Setup game objects
        var swordObj = new GameObject("Sword");
        var sword = swordObj.AddComponent<PlasmaSword>();
        sword.attacker = new RuntimeStats { baseAttack = 10 };

        var targetObj = new GameObject("Target");
        var targetStats = new RuntimeStats { baseDefense = 5 };
        var health = targetObj.AddComponent<Health>();
        health.maxHealth = 100;
        health.currentHealth = 100;

        sword.targetStats = targetStats;
        sword.targetHealth = health;

        var ability = ScriptableObject.CreateInstance<AbilityData>();
        ability.baseDamage = 20f;
        ability.energyCost = 0;
        ability.animationClip = null;

        // Calculate expected damage with deterministic random
        Random.InitState(0);
        float dmg = BattleFormula.CalculateDamage(sword.attacker, targetStats, ability);
        int expected = Mathf.RoundToInt(dmg);

        // Reset random state before invoking private method
        Random.InitState(0);
        typeof(PlasmaSword).GetMethod("PerformAttack", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(sword, new object[] { ability });

        Assert.AreEqual(100 - expected, health.currentHealth);
    }
}
