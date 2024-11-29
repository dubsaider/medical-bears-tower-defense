using System;
using UnityEngine;

namespace Code.Controllers
{
    public class UIMainMenuController : MonoBehaviour
    {
        private UIMainMenuRenderer _uiRenderer;

        public void OpenLevels()
        {
            HideAllPanels();
            _uiRenderer.ShowLevelsPanel();
        }

        public void CloseLevels() => OpenMainPanel();

        public void OpenBearPedia()
        {
            HideAllPanels();
            _uiRenderer.ShowBearPediaPanel();
        }

        public void CloseBearPedia() => OpenMainPanel();

        private void OpenMainPanel()
        {
            HideAllPanels();
            _uiRenderer.ShowMainPanel();
        }

        private void HideAllPanels()
        {
            _uiRenderer.HideMainPanel();
            _uiRenderer.HideLevelsPanel();
            _uiRenderer.HideBearPediaPanel();
        }
        
        private void Awake()
        {
            _uiRenderer = GetComponent<UIMainMenuRenderer>();
        }

        private void OnEnable()
        {
            OpenMainPanel();
        }
    }
}