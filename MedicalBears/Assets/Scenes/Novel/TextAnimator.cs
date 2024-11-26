using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Image characterImage;
    public float characterDelay = 0.05f;

    private string[][] dialogues; // Массив массивов для диалогов разных персонажей
    private Sprite[] characterSprites; // Массив спрайтов для каждого персонажа
    private int currentDialogueIndex = 0;
    private int currentCharacterIndex = 0;
    private Coroutine typingCoroutine;
    private bool isWaitingForInput = false;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshProUGUI не найден!");
            }
        }

        if (characterImage == null)
        {
            characterImage = GetComponent<Image>();
            if (characterImage == null)
            {
                Debug.LogError("Image не найден!");
            }
        }
    }

    public void SetDialoguesAndSprites(string[][] dialogues, Sprite[] characterSprites)
    {
        this.dialogues = dialogues;
        this.characterSprites = characterSprites;
        currentDialogueIndex = 0;
        currentCharacterIndex = 0;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeDialogues());
    }

    private IEnumerator TypeDialogues()
    {
        while (currentCharacterIndex < dialogues.Length)
        {
            while (currentDialogueIndex < dialogues[currentCharacterIndex].Length)
            {
                Debug.Log("Начинаем вывод строки: " + dialogues[currentCharacterIndex][currentDialogueIndex]);
                yield return StartCoroutine(TypeText(dialogues[currentCharacterIndex][currentDialogueIndex]));
                currentDialogueIndex++;

                // Если это не первая строка, ждем нажатия кнопки
                if (currentDialogueIndex > 0)
                {
                    isWaitingForInput = true;
                    Debug.Log("Ожидание нажатия кнопки...");
                    while (isWaitingForInput)
                    {
                        yield return null;
                    }
                    Debug.Log("Кнопка нажата, продолжаем...");
                }
            }

            // Переключаемся на следующего персонажа
            currentCharacterIndex++;
            currentDialogueIndex = 0;

            // Меняем картинку персонажа
            if (currentCharacterIndex < characterSprites.Length)
            {
                characterImage.sprite = characterSprites[currentCharacterIndex];
            }
        }
    }

    private IEnumerator TypeText(string text)
    {
        // Стираем предыдущий текст
        textMeshPro.text = "";

        foreach (char c in text)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(characterDelay);

            // Проверка на переполнение
            if (textMeshPro.isTextOverflowing)
            {
                Debug.LogWarning("Текст переполнен!");
                yield break;
            }
        }

        // Ожидание перед удалением текста
        yield return new WaitForSeconds(1.0f);
    }

    // Добавляем модификатор public, чтобы метод был виден в инспекторе
    public void NextDialogue()
    {
        if (isWaitingForInput)
        {
            Debug.Log("Кнопка нажата, продолжаем вывод текста.");
            isWaitingForInput = false;
        }
    }
}