using UnityEngine;
using UnityEngine.Playables;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Plays a timeline cutscene when the player enters the trigger volume.
    /// </summary>
    public class CutsceneTrigger : TriggerVolume
    {
        [Tooltip("Timeline played when the player enters.")]
        public PlayableDirector director;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.CompareTag("Player") && director != null)
            {
                director.Play();
            }
        }
    }
}
