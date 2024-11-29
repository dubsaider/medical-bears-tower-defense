using UnityEngine;

public class ParallaxAndSwayBackground : MonoBehaviour
{
    public Transform cameraTransform; // —сылка на трансформ камеры
    public float parallaxEffect = 0.1f; //  оэффициент параллакса (чем меньше, тем сильнее эффект)
    public float swayAmplitude = 1.0f; // јмплитуда качани€
    public float swayFrequency = 1.0f; // „астота качани€

    private Vector3 lastCameraPosition;
    private Vector3 initialCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
        initialCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // Ёффект параллакса
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffect;
        lastCameraPosition = cameraTransform.position;

        // Ёффект качани€
        float swayOffset = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;
        Vector3 swayPosition = initialCameraPosition + new Vector3(swayOffset, 0, 0);
        cameraTransform.position = swayPosition;
    }
}