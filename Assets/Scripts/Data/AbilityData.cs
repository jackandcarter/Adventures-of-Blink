using UnityEngine;

namespace AdventuresOfBlink.Data
{
    /// <summary>
    /// Describes an ability that Blink or other characters can perform.
    /// Stored as a ScriptableObject for easy editing in the Unity Inspector.
    /// </summary>
    [CreateAssetMenu(fileName = "AbilityData", menuName = "AdventuresOfBlink/Ability", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [Header("Basic Info")]
        public string abilityName;
        public Sprite icon;
        [TextArea]
        public string description;

        [Header("Input")]
        [Tooltip("Keyboard slot index 0-9 for quick access. -1 means unassigned.")]
        public int keyboardSlot = -1;

        [Tooltip("Optional input action name for controller profiles.")]
        public string inputAction;

        [Header("Combat")]
        public float baseDamage = 1f;
        public int energyCost = 0;
        public float cooldown = 0f;
        public AnimationClip animationClip;
    }
}
