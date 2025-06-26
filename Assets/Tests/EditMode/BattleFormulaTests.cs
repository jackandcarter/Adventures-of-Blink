using NUnit.Framework;
using UnityEngine;
using AdventuresOfBlink.Data;
using AdventuresOfBlink.Combat;

public class BattleFormulaTests
{
    [Test]
    public void CalculateDamage_JitterWithinTenPercent()
    {
        var ability = ScriptableObject.CreateInstance<AbilityData>();
        ability.baseDamage = 10f;

        var attacker = new RuntimeStats { baseAttack = 10 };
        var defender = new RuntimeStats { baseDefense = 10 };

        float expected = (attacker.Attack / (float)defender.Defense) * ability.baseDamage;

        for (int i = 0; i < 50; i++)
        {
            Random.InitState(i);
            float damage = BattleFormula.CalculateDamage(attacker, defender, ability);
            Assert.GreaterOrEqual(damage, expected * 0.9f);
            Assert.LessOrEqual(damage, expected * 1.1f);
        }
    }

    [Test]
    public void CalculateDamage_ScalesWithAttack()
    {
        var ability = ScriptableObject.CreateInstance<AbilityData>();
        ability.baseDamage = 10f;

        var attacker1 = new RuntimeStats { baseAttack = 10 };
        var attacker2 = new RuntimeStats { baseAttack = 20 };
        var defender = new RuntimeStats { baseDefense = 5 };

        Random.InitState(0);
        float dmg1 = BattleFormula.CalculateDamage(attacker1, defender, ability);
        Random.InitState(0);
        float dmg2 = BattleFormula.CalculateDamage(attacker2, defender, ability);

        Assert.AreEqual(dmg1 * 2f, dmg2, 0.001f);
    }
}
