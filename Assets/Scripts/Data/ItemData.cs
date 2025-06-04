using UnityEngine;

namespace AdventuresOfBlink.Data
{
    /// <summary>
    /// Represents an item that can be used in or out of battle.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "AdventuresOfBlink/Item", order = 1)]
    public class ItemData : ScriptableObject
    {
        [Header("Basic Info")]
        public string itemName;
        public Sprite icon;
        [TextArea]
        public string description;

        [Header("Gameplay")]
        public bool consumable = true;
        public int maxStack = 1;
    }
}
