using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundButton : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public AudioSource audioSource;

    private Button button;
    private Image buttonImage;
    private bool isSoundOn = true;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogError("Image component not found on the button!");
        }

        buttonImage.sprite = soundOnSprite; // ������������� ��������� ������
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        if (isSoundOn)
        {
            audioSource.enabled = true; // �������� ����
            buttonImage.sprite = soundOnSprite; // ������ ������ �� "SoundOn"
        }
        else
        {
            audioSource.enabled = false; // ��������� ����
            buttonImage.sprite = soundOffSprite; // ������ ������ �� "SoundOff"
        }
    }
}