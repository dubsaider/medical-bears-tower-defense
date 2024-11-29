using UnityEngine;

public class UIMainMenuRenderer : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private GameObject BearPediaPanel;

    public void ShowMainPanel() => MainPanel.SetActive(true);
    public void HideMainPanel() => MainPanel.SetActive(false);

    public void ShowLevelsPanel() => LevelsPanel.SetActive(true);
    public void HideLevelsPanel() => LevelsPanel.SetActive(false);

    public void ShowBearPediaPanel() => BearPediaPanel.SetActive(true);
    public void HideBearPediaPanel() => BearPediaPanel.SetActive(false);
}
