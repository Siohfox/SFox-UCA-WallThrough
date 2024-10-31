using UnityEngine;

public class PulseGlow : MonoBehaviour
{
    public float pulseSpeed = 1.0f; // Speed of the pulsing
    public float maxIntensity = 1.0f; // Maximum intensity of the glow
    public Color emissionColor = new Color(100f / 255f, 100f / 255f, 0f); // Emission color

    private Material material;

    void Start()
    {
        // Get the Renderer component and the material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
    }

    void Update()
    {
        if (material != null)
        {
            // Calculate the new intensity based on a sine wave
            float intensity = Mathf.PingPong(Time.time * pulseSpeed, maxIntensity);
            Color newEmissionColor = emissionColor * intensity;
            material.SetColor("_EmissionColor", newEmissionColor);
        }
    }
}
