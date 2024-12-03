using Code.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core
{
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] private int levelIndex;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(LoadLevelByIndex);
        }

        private void OnEnable()
        {
            var lastPassedLevelIndex = SaveLoadHandler.GetLastPassedLevelIndex();

            if (levelIndex <= lastPassedLevelIndex)
                UpdateButton(_button, Color.green, true);
            else if (levelIndex == lastPassedLevelIndex + 1)
                UpdateButton(_button, Color.white, true);
            else
                UpdateButton(_button, Color.gray, false);
        }

        private void UpdateButton(Button button, Color color, bool isInteractable)
        {
            button.image.color = color;
            button.interactable = isInteractable;
        }

        private void LoadLevelByIndex()
        {
            // Загрузка уровня по индексу
            Debug.Log("Loading Level " + (levelIndex + 1));
            UIController.Instance.StartSelectedLevel(levelIndex);
        }
    }
}