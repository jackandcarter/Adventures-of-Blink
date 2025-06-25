using NUnit.Framework;
using UnityEngine;
using AdventuresOfBlink;
using AdventuresOfBlink.Data;
using AdventuresOfBlink.Systems;
using AdventuresOfBlink.Companion;

public class UpgradeSystemTests
{
    [Test]
    public void ApplyUpgrade_ConsumesMaterialsAndUpgradesDuke()
    {
        var go = new GameObject("UpgradeSystem");
        var inventory = go.AddComponent<InventorySystem>();
        var forms = go.AddComponent<PlayerFormSwitcher>();
        var duke = go.AddComponent<DukeController>();
        var system = go.AddComponent<UpgradeSystem>();
        system.inventory = inventory;
        system.playerForms = forms;
        system.duke = duke;

        var material = ScriptableObject.CreateInstance<ItemData>();
        inventory.AddItem(material, 5);

        var abilityData = ScriptableObject.CreateInstance<DukeAbilityData>();
        duke.abilities.Add(new DukeController.DukeAbilitySlot
        {
            data = abilityData,
            level = 1
        });

        var upgrade = ScriptableObject.CreateInstance<UpgradeData>();
        upgrade.requirements = new[]
        {
            new UpgradeData.Requirement { item = material, amount = 3 }
        };
        upgrade.dukeAbilityIndex = 0;

        bool result = system.ApplyUpgrade(upgrade);

        Assert.IsTrue(result);
        Assert.AreEqual(2, inventory.items.Find(e => e.data == material).quantity);
        Assert.AreEqual(2, duke.abilities[0].level);
    }
}
