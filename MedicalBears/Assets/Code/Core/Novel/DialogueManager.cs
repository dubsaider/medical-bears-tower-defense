using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using Code.Controllers;
using Code.Core;
using UnityEngine.UI;
using Code.Entities;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Image leftCharacterImage;
    public Image rightCharacterImage;
    public Image backgroundImage; // Фоновое изображение
    public Canvas canvas; // Канвас

    private DialogueSession _currentDialogueSession;

    private int currentDialogueIndex = 0;
    private string _currentPhrase;
    private int _currentCharIndex = 0;
    private float typeSpeed = 0.05f; // Скорость анимации текста (в секундах на символ)
    private float backgroundMoveSpeed = 2f; // Скорость смещения фона

    public void Init(DialogueSession dialogueSession)
    {
        _currentDialogueSession = dialogueSession;
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (_currentDialogueSession != null && currentDialogueIndex < _currentDialogueSession.dialogueLines.Count)
        {
            _currentPhrase = _currentDialogueSession.dialogueLines[currentDialogueIndex].dialogueText;
            _currentCharIndex = 0;
            StopAllCoroutines(); // Останавливаем предыдущие корутины
            StartCoroutine(TypeText());

            // Установка спрайта персонажа
            int characterNumber = _currentDialogueSession.dialogueLines[currentDialogueIndex].characterNumber;
            Character character = _currentDialogueSession.characters.Find(c => c.characterNumber == characterNumber);
            if (character != null && character.characterSprite != null)
            {
                if (characterNumber % 2 == 0)
                {
                    leftCharacterImage.sprite = character.characterSprite;
                    leftCharacterImage.color = Color.white; // Возвращаем полную непрозрачность
                    rightCharacterImage.sprite = null;
                    rightCharacterImage.color = new Color(1, 1, 1, 0); // Делаем полностью прозрачным
                }
                else
                {
                    rightCharacterImage.sprite = character.characterSprite;
                    rightCharacterImage.color = Color.white; // Возвращаем полную непрозрачность
                    leftCharacterImage.sprite = null;
                    leftCharacterImage.color = new Color(1, 1, 1, 0); // Делаем полностью прозрачным
                }
            }
        }
        else
        {
            CoreEventsProvider.NovelFinished.Invoke();
        }
    }

    IEnumerator TypeText()
    {
        while (_currentCharIndex <= _currentPhrase.Length)
        {
            dialogueText.text = _currentPhrase.Substring(0, _currentCharIndex);
            _currentCharIndex++;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // Метод для вызова следующей части диалога через кнопку или событие
    public void OnClick_NextPart()
    {
        if (_currentCharIndex < _currentPhrase.Length)
        {
            _currentCharIndex = _currentPhrase.Length; // Быстро вывести оставшуюся часть текста
            dialogueText.text = _currentPhrase; // Установить полный текст диалога
        }
        else if (currentDialogueIndex < _currentDialogueSession.dialogueLines.Count - 1)
        {
            currentDialogueIndex++;
            StartDialogue();
        }
        else
        {
            CoreEventsProvider.NovelFinished.Invoke();
        }
    }

    // Метод для сброса счетчика диалогов
    private void ResetDialogueIndex()
    {
        currentDialogueIndex = 0;
    }

    private void OnEnable()
    {
        CoreEventsProvider.NovelFinished += ResetDialogueIndex;
    }

    private void OnDisable()
    {
        CoreEventsProvider.NovelFinished -= ResetDialogueIndex;
    }
}