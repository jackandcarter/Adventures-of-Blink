using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Loads the specified scene when the player enters the trigger volume.
    /// </summary>
    public class SceneChangeTrigger : TriggerVolume
    {
        [Tooltip("Name of the scene to load on enter.")]
        public string sceneName;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.CompareTag("Player") && !string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
