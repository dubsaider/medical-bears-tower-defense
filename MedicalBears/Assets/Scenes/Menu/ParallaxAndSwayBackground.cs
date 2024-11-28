using UnityEngine;

public class ParallaxAndSwayBackground : MonoBehaviour
{
    public Transform cameraTransform; // ������ �� ��������� ������
    public float parallaxEffect = 0.1f; // ����������� ���������� (��� ������, ��� ������� ������)
    public float swayAmplitude = 1.0f; // ��������� �������
    public float swayFrequency = 1.0f; // ������� �������

    private Vector3 lastCameraPosition;
    private Vector3 initialCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
        initialCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // ������ ����������
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffect;
        lastCameraPosition = cameraTransform.position;

        // ������ �������
        float swayOffset = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;
        Vector3 swayPosition = initialCameraPosition + new Vector3(swayOffset, 0, 0);
        cameraTransform.position = swayPosition;
    }
}