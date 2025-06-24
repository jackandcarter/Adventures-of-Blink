using UnityEngine;

namespace AdventuresOfBlink.Data
{
    /// <summary>
    /// Defines an ability that Duke can perform with upgrade parameters.
    /// </summary>
    [CreateAssetMenu(fileName = "DukeAbilityData", menuName = "AdventuresOfBlink/Duke Ability", order = 4)]
    public class DukeAbilityData : ScriptableObject
    {
        [Header("Basic Info")]
        public string abilityName;
        public Sprite icon;
        [TextArea]
        public string description;

        [Header("Upgrade")]
        [Tooltip("Maximum upgrade level for this ability.")]
        public int maxLevel = 1;

        [Tooltip("Base cooldown in seconds.")]
        public float cooldown = 0f;

        [Header("Animation")]
        public AnimationClip animationClip;
    }
}
