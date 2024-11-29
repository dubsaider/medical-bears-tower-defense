using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIElementButtonPair
{
    public GameObject uiElement;
    public Button button;
}

public class UIManagerScript : MonoBehaviour
{
    public List<UIElementButtonPair> uiElementButtonPairs;

    void Start()
    {
        foreach (var pair in uiElementButtonPairs)
        {
            pair.button.onClick.AddListener(() => ToggleUIElement(pair.uiElement));
        }
    }

    void ToggleUIElement(GameObject uiElementToToggle)
    {
        foreach (var pair in uiElementButtonPairs)
        {
            pair.uiElement.SetActive(false);
        }
        uiElementToToggle.SetActive(!uiElementToToggle.activeSelf);
    }
}