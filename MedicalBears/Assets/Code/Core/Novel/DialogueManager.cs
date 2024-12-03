using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using Code.Controllers;
using UnityEngine.UI;
using Code.Entities;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Image leftCharacterImage;
    public Image rightCharacterImage;
    public Image backgroundImage; // Фоновое изображение
    public Canvas canvas; // Канвас

    [SerializeField]
    public DialogueSession currentDialogueSession;

    private int currentDialogueIndex = 0;
    private string currentDialogue;
    private int currentCharIndex = 0;
    private float typeSpeed = 0.05f; // Скорость анимации текста (в секундах на символ)
    private float backgroundMoveSpeed = 2f; // Скорость смещения фона

    void Start()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (currentDialogueSession != null && currentDialogueIndex < currentDialogueSession.dialogueLines.Count)
        {
            currentDialogue = currentDialogueSession.dialogueLines[currentDialogueIndex].dialogueText;
            currentCharIndex = 0;
            StopAllCoroutines(); // Останавливаем предыдущие корутины
            StartCoroutine(TypeText());

            // Установка спрайта персонажа
            int characterNumber = currentDialogueSession.dialogueLines[currentDialogueIndex].characterNumber;
            Character character = currentDialogueSession.characters.Find(c => c.characterNumber == characterNumber);
            if (character != null && character.characterSprite != null)
            {
                if (characterNumber % 2 == 0)
                {
                    leftCharacterImage.sprite = character.characterSprite;
                    leftCharacterImage.color = Color.white; // Возвращаем полную непрозрачность
                    rightCharacterImage.sprite = null;
                    rightCharacterImage.color = new Color(1, 1, 1, 0); // Делаем полностью прозрачным
                    //StartCoroutine(MoveBackground(true)); // Смещаем фон влево
                }
                else
                {
                    rightCharacterImage.sprite = character.characterSprite;
                    rightCharacterImage.color = Color.white; // Возвращаем полную непрозрачность
                    leftCharacterImage.sprite = null;
                    leftCharacterImage.color = new Color(1, 1, 1, 0); // Делаем полностью прозрачным
                    //StartCoroutine(MoveBackground(false)); // Смещаем фон вправо
                }
            }
        }
        else
        {
            Debug.Log("Диалог завершен");
        }
    }

    IEnumerator TypeText()
    {
        while (currentCharIndex <= currentDialogue.Length)
        {
            dialogueText.text = currentDialogue.Substring(0, currentCharIndex);
            currentCharIndex++;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // Метод для вызова следующей части диалога через кнопку или событие
    public void OnClick_NextPart()
    {
        if (currentCharIndex < currentDialogue.Length)
        {
            currentCharIndex = currentDialogue.Length; // Быстро вывести оставшуюся часть текста
            dialogueText.text = currentDialogue; // Установить полный текст диалога
        }
        else if (currentDialogueIndex < currentDialogueSession.dialogueLines.Count - 1)
        {
            currentDialogueIndex++;
            StartDialogue();
        }
        else
        {
            UIController.Instance.StartNewGame();
        }
    }
}