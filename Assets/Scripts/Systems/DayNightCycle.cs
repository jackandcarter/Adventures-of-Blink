using UnityEngine;

namespace AdventuresOfBlink.Systems
{
    /// <summary>
    /// Manages the in-game time of day and updates scene lighting.
    /// One full day lasts <see cref="dayLength"/> seconds and wraps
    /// automatically. Color and intensity curves control the light.
    /// </summary>
    public class DayNightCycle : MonoBehaviour
    {
        [Tooltip("Directional light acting as the sun.")]
        public Light sunLight;

        [Tooltip("Gradient mapping time of day to light color.")]
        public Gradient lightColor = new Gradient();

        [Tooltip("Curve mapping time of day to light intensity.")]
        public AnimationCurve lightIntensity = AnimationCurve.Linear(0f, 1f, 1f, 0f);

        [Tooltip("Length of a full day in seconds.")]
        public float dayLength = 300f;

        [Tooltip("Current time of day normalized 0-1.")]
        [Range(0f, 1f)]
        public float timeOfDay;

        /// <summary>
        /// Returns true if the current time is considered nighttime.
        /// </summary>
        public bool IsNight => timeOfDay >= 0.5f;

        private void Update()
        {
            timeOfDay += Time.deltaTime / Mathf.Max(1f, dayLength);
            if (timeOfDay > 1f)
                timeOfDay -= 1f;
            UpdateLighting();
        }

        private void UpdateLighting()
        {
            if (sunLight == null)
                return;

            sunLight.color = lightColor.Evaluate(timeOfDay);
            sunLight.intensity = lightIntensity.Evaluate(timeOfDay);
            float angle = timeOfDay * 360f - 90f;
            sunLight.transform.rotation = Quaternion.Euler(angle, 170f, 0f);
        }
    }
}
