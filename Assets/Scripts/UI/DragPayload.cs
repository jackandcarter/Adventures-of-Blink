using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Shared payload used during UI drag-and-drop operations.
    /// Stores either an ability or item being dragged and the source slot.
    /// </summary>
    public static class DragPayload
    {
        public static AbilityData draggedAbility;
        public static ItemData draggedItem;
        public static DockSlotUI sourceSlot;

        /// <summary>
        /// Returns true if an item or ability is currently being dragged.
        /// </summary>
        public static bool HasPayload => draggedAbility != null || draggedItem != null;

        /// <summary>
        /// Clears the current drag payload.
        /// </summary>
        public static void Clear()
        {
            draggedAbility = null;
            draggedItem = null;
            sourceSlot = null;
        }
    }
}
