using UnityEngine;

namespace AdventuresOfBlink.Targeting
{
    /// <summary>
    /// Marks an object that can be targeted by the <see cref="TargetingSystem"/>.
    /// </summary>
    public class Targetable : MonoBehaviour
    {
        [Tooltip("Name shown in the UI when this object is targeted.")]
        public string displayName;

    }
}
