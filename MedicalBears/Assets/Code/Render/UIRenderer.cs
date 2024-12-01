using UnityEngine;

namespace Code.Render
{
    public class UIRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenuUI;
        [SerializeField] private GameObject GameUI;

        [SerializeField] private GameObject PauseMenu;
        [SerializeField] private GameObject VictoryMenu;
        [SerializeField] private GameObject DefeatMenu;

        public void ShowMainMenuUI()
        {
            MainMenuUI.SetActive(true);
        }
        
        public void HideMainMenuUI()
        {
            MainMenuUI.SetActive(false);
        }
        
        public void ShowGameUI()
        {
            GameUI.SetActive(true);
        }
        
        public void HideGameUI()
        {
            GameUI.SetActive(false);
            
            HidePauseMenu();
            HideDefeatMenu();
            HideVictoryMenu();
        }
        
        public void ShowPauseMenu()
        {
            PauseMenu.SetActive(true);
        }
        
        public void HidePauseMenu()
        {
            PauseMenu.SetActive(false);
        }
        
        public void ShowVictoryMenu()
        {
            VictoryMenu.SetActive(true);
        }
        
        public void HideVictoryMenu()
        {
            VictoryMenu.SetActive(false);
        }
        
        public void ShowDefeatMenu()
        {
            DefeatMenu.SetActive(true);
        }
        
        public void HideDefeatMenu()
        {
            DefeatMenu.SetActive(false);
        }
    }
}