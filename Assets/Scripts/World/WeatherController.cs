using UnityEngine;
using UnityEngine.Events;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Controls dynamic weather effects like rain and adjusts scene lighting.
    /// Other systems can subscribe to <see cref="WeatherChanged"/> or
    /// <see cref="onWeatherChanged"/> to react when the weather toggles.
    /// </summary>
    public class WeatherController : MonoBehaviour
    {
        [Tooltip("Directional light influenced by the weather.")]
        public Light sunLight;

        [Tooltip("Particle system prefab used for rain.")]
        public ParticleSystem rainPrefab;

        [Tooltip("Ambient color when the sky is clear.")]
        public Color clearAmbientColor = Color.white;

        [Tooltip("Ambient color applied during rain.")]
        public Color rainAmbientColor = Color.gray;

        [Tooltip("Multiplier applied to sunlight intensity when raining.")]
        public float rainLightMultiplier = 0.7f;

        /// <summary>
        /// Current active weather type.
        /// </summary>
        public WeatherType CurrentWeather { get; private set; } = WeatherType.Clear;

        /// <summary>
        /// Raised whenever the weather changes.
        /// </summary>
        public event System.Action<WeatherType> WeatherChanged;

        /// <summary>
        /// UnityEvent version of <see cref="WeatherChanged"/> for inspector hooks.
        /// </summary>
        [System.Serializable]
        public class WeatherEvent : UnityEvent<WeatherType> { }
        public WeatherEvent onWeatherChanged = new WeatherEvent();

        private ParticleSystem activeRain;
        private float baseLightIntensity;

        private void Awake()
        {
            if (sunLight != null)
                baseLightIntensity = sunLight.intensity;
        }

        private void Start()
        {
            ApplyWeatherEffects();
        }

        /// <summary>
        /// Switches to the specified weather type.
        /// </summary>
        public void SetWeather(WeatherType newWeather)
        {
            if (CurrentWeather == newWeather)
                return;

            CurrentWeather = newWeather;
            ApplyWeatherEffects();
            WeatherChanged?.Invoke(CurrentWeather);
            onWeatherChanged.Invoke(CurrentWeather);
        }

        private void ApplyWeatherEffects()
        {
            bool raining = CurrentWeather == WeatherType.Rain;

            if (raining)
            {
                if (rainPrefab != null && activeRain == null)
                {
                    activeRain = Instantiate(rainPrefab, transform);
                }
                if (activeRain != null)
                    activeRain.gameObject.SetActive(true);
            }
            else if (activeRain != null)
            {
                activeRain.gameObject.SetActive(false);
            }

            if (sunLight != null)
            {
                sunLight.intensity = baseLightIntensity * (raining ? rainLightMultiplier : 1f);
            }

            RenderSettings.ambientLight = raining ? rainAmbientColor : clearAmbientColor;
        }
    }

    /// <summary>
    /// Possible weather types used by <see cref="WeatherController"/>.
    /// </summary>
    public enum WeatherType
    {
        Clear,
        Rain
    }
}
