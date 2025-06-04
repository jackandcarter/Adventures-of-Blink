using System.Collections.Generic;
using UnityEngine;

namespace AdventuresOfBlink.Combat
{
    /// <summary>
    /// Defines a sequence of abilities that form a combo attack.
    /// </summary>
    [CreateAssetMenu(fileName = "ComboSequence", menuName = "AdventuresOfBlink/Combo Sequence", order = 3)]
    public class ComboSequence : ScriptableObject
    {
        public List<AbilityData> steps = new List<AbilityData>();

        [Tooltip("Time in seconds within which the next input must occur.")]
        public float inputWindow = 0.5f;
    }
}
