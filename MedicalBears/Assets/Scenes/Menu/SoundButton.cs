using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundButton : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private Button button;
    private Image buttonImage;
    private bool isSoundOn = true;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        buttonImage.sprite = soundOnSprite; // Устанавливаем начальный спрайт
        button.onClick.AddListener(ToggleSound);
    }

    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        buttonImage.sprite = isSoundOn 
            ? soundOnSprite 
            : soundOffSprite; 
    }
}