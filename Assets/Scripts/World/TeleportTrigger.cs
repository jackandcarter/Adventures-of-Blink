using UnityEngine;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Teleports the player to a target location when entering the trigger.
    /// </summary>
    public class TeleportTrigger : TriggerVolume
    {
        [Tooltip("Destination transform to move the player to.")]
        public Transform targetLocation;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.CompareTag("Player") && targetLocation != null)
            {
                other.transform.position = targetLocation.position;
            }
        }
    }
}
