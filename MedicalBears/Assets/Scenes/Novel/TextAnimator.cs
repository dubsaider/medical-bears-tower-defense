using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float characterDelay = 0.05f;

    private string[] dialogues;
    private int currentDialogueIndex = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetDialogues(string[] dialogues)
    {
        this.dialogues = dialogues;
        currentDialogueIndex = 0;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeDialogues());
    }

    private IEnumerator TypeDialogues()
    {
        while (true)
        {
            while (currentDialogueIndex < dialogues.Length)
            {
                yield return StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
                currentDialogueIndex++;
            }
            currentDialogueIndex = 0; // Сброс индекса для бесконечного цикла
        }
    }

    private IEnumerator TypeText(string text)
    {
        textMeshPro.text = "";
        foreach (char c in text)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(characterDelay);

            // Проверка на переполнение
            if (textMeshPro.isTextOverflowing)
            {
                textMeshPro.text = "";
                yield break;
            }
        }

        // Ожидание перед удалением текста
        yield return new WaitForSeconds(1.0f);

        // Удаление текста
        textMeshPro.text = "";
    }
}