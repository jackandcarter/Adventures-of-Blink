using NUnit.Framework;
using UnityEngine;
using AdventuresOfBlink;
using AdventuresOfBlink.World;
using AdventuresOfBlink.Data;

public class WorldItemTests
{
    [Test]
    public void PickUp_AddsItemAndDestroysObject()
    {
        var item = ScriptableObject.CreateInstance<ItemData>();
        var inventoryObj = new GameObject("Inventory");
        var inventory = inventoryObj.AddComponent<InventorySystem>();

        var go = new GameObject("WorldItem");
        var worldItem = go.AddComponent<WorldItem>();
        worldItem.itemData = item;
        worldItem.quantity = 2;

        worldItem.PickUp(inventory);

        Assert.AreEqual(1, inventory.items.Count);
        Assert.AreEqual(item, inventory.items[0].data);
        Assert.AreEqual(2, inventory.items[0].quantity);
        Assert.IsTrue(worldItem == null);
    }
}
