using UnityEngine;
using AdventuresOfBlink.Companion;

namespace AdventuresOfBlink.Systems
{
    /// <summary>
    /// Component that loads saved data on startup and
    /// writes it back when the application quits.
    /// </summary>
    public class SaveGame : MonoBehaviour
    {
        [Tooltip("Inventory system to persist.")]
        public InventorySystem inventory;

        [Tooltip("Duke controller for upgrade levels.")]
        public DukeController duke;

        private void Start()
        {
            SaveUtility.Load(inventory, duke);
        }

        private void OnApplicationQuit()
        {
            SaveUtility.Save(inventory, duke);
        }
    }
}
