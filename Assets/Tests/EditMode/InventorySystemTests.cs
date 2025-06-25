using NUnit.Framework;
using UnityEngine;
using AdventuresOfBlink;
using AdventuresOfBlink.Data;

public class InventorySystemTests
{
    [Test]
    public void RemoveItem_ReturnsFalse_WhenQuantityExceedsInventory()
    {
        var gameObject = new GameObject("InventorySystem");
        var inventory = gameObject.AddComponent<InventorySystem>();
        var item = ScriptableObject.CreateInstance<ItemData>();
        inventory.AddItem(item, 1);

        bool result = inventory.RemoveItem(item, 2);

        Assert.IsFalse(result);
        Assert.AreEqual(1, inventory.items.Find(e => e.data == item).quantity);
    }
}
