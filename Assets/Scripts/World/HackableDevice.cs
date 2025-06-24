using UnityEngine;
using UnityEngine.Events;
using AdventuresOfBlink.Player;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Marks an object that can be hacked by Blink. A hack may either
    /// be a quick timed interaction or launch a minigame.
    /// </summary>
    public class HackableDevice : MonoBehaviour
    {
        [Tooltip("Time in seconds required for a quick hack.")]
        public float hackTime = 2f;

        [Tooltip("If true a hacking minigame should be launched.")]
        public bool useMinigame;

        [Tooltip("Invoked when hacking begins.")]
        public UnityEvent onHackStart;

        [Tooltip("Invoked when hacking completes.")]
        public UnityEvent onHackComplete;

        /// <summary>
        /// Returns true if this device has already been hacked.
        /// </summary>
        public bool IsHacked { get; private set; }

        /// <summary>
        /// Called by <see cref="PlayerHacking"/> to trigger the hack.
        /// </summary>
        public void BeginHack(PlayerHacking hacker)
        {
            if (IsHacked || hacker == null)
                return;

            if (useMinigame)
            {
                onHackStart?.Invoke();
                Debug.Log($"Launching hacking minigame for {name}");
                // A real minigame would be triggered here.
                CompleteHack();
            }
            else
            {
                hacker.StartTimedHack(this);
            }
        }

        /// <summary>
        /// Completes the hack and invokes events.
        /// </summary>
        public void CompleteHack()
        {
            if (IsHacked)
                return;

            IsHacked = true;
            onHackComplete?.Invoke();
            Debug.Log($"{name} successfully hacked");
        }
    }
}
