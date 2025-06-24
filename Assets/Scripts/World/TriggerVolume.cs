using UnityEngine;
using UnityEngine.Events;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Base trigger volume that detects the player using OnTrigger events.
    /// Requires a collider set as a trigger.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerVolume : MonoBehaviour
    {
        [Tooltip("Invoked when the player enters this volume.")]
        public UnityEvent onPlayerEnter;

        [Tooltip("Invoked when the player exits this volume.")]
        public UnityEvent onPlayerExit;

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.isTrigger = true;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                onPlayerEnter?.Invoke();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                onPlayerExit?.Invoke();
        }
    }
}
