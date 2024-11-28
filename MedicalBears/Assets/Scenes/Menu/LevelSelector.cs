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
                    SetButtonColor(levelButtons[i], Color.green); // ������������� ������� ���� ��� ���������� �������
                }
                else
                {
                    levelButtons[i].interactable = false;
                    SetButtonColor(levelButtons[i], Color.gray); // ������������� ����� ���� ��� ����������� �������
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
            // �������� ������ �� �������
            Debug.Log("Loading Level " + (levelIndex + 1));
            // ����� ����� �������� ������ �������� ������, ��������, ����� SceneManager.LoadScene
        }
    }
}