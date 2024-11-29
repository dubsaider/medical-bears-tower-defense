using UnityEngine;
using UnityEngine.UI;

namespace Code.Core
{
    public class LevelSelector : MonoBehaviour
    {
        public Button[] levelButtons;

        void Start()
        {
            int lastPassedLevelIndex = SaveLoadHandler.GetLastPassedLevelIndex();

            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i <= lastPassedLevelIndex)
                {
                    levelButtons[i].interactable = true;
                    SetButtonColor(levelButtons[i], Color.green); // Устанавливаем зеленый цвет для пройденных уровней
                }
                else
                {
                    levelButtons[i].interactable = false;
                    SetButtonColor(levelButtons[i], Color.gray); // Устанавливаем серый цвет для недоступных уровней
                }
            }
        }

        private void SetButtonColor(Button button, Color color)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = color;
            colors.highlightedColor = color;
            colors.pressedColor = color;
            colors.selectedColor = color;
            button.colors = colors;
        }

        public void LoadLevelByIndex(int levelIndex)
        {
            // Загрузка уровня по индексу
            Debug.Log("Loading Level " + (levelIndex + 1));
            // Здесь можно добавить логику загрузки уровня, например, через SceneManager.LoadScene
        }
    }
}