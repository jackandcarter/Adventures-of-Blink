using UnityEngine;

namespace AdventuresOfBlink.Effects
{
    /// <summary>
    /// Applies the hologram barrier material to a mesh when Blink transforms.
    /// The material should use the HologramBarrier shader.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class BlinkHologram : MonoBehaviour
    {
        [Tooltip("Material using the HologramBarrier shader.")]
        public Material hologramMaterial;

        private Material originalMaterial;
        private Renderer rend;

        private void Awake()
        {
            rend = GetComponent<Renderer>();
            originalMaterial = rend.sharedMaterial;
        }

        /// <summary>
        /// Enables or disables the hologram effect.
        /// </summary>
        public void SetActive(bool active)
        {
            if (rend == null || hologramMaterial == null)
                return;
            rend.material = active ? hologramMaterial : originalMaterial;
        }
    }
}
