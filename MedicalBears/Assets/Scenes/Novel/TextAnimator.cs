using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Image characterImage;
    public float characterDelay = 0.05f;

    private string[][] dialogues; // ������ �������� ��� �������� ������ ����������
    private Sprite[] characterSprites; // ������ �������� ��� ������� ���������
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
                Debug.LogError("TextMeshProUGUI �� ������!");
            }
        }

        if (characterImage == null)
        {
            characterImage = GetComponent<Image>();
            if (characterImage == null)
            {
                Debug.LogError("Image �� ������!");
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
                Debug.Log("�������� ����� ������: " + dialogues[currentCharacterIndex][currentDialogueIndex]);
                yield return StartCoroutine(TypeText(dialogues[currentCharacterIndex][currentDialogueIndex]));
                currentDialogueIndex++;

                // ���� ��� �� ������ ������, ���� ������� ������
                if (currentDialogueIndex > 0)
                {
                    isWaitingForInput = true;
                    Debug.Log("�������� ������� ������...");
                    while (isWaitingForInput)
                    {
                        yield return null;
                    }
                    Debug.Log("������ ������, ����������...");
                }
            }

            // ������������� �� ���������� ���������
            currentCharacterIndex++;
            currentDialogueIndex = 0;

            // ������ �������� ���������
            if (currentCharacterIndex < characterSprites.Length)
            {
                characterImage.sprite = characterSprites[currentCharacterIndex];
            }
        }
    }

    private IEnumerator TypeText(string text)
    {
        // ������� ���������� �����
        textMeshPro.text = "";

        foreach (char c in text)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(characterDelay);

            // �������� �� ������������
            if (textMeshPro.isTextOverflowing)
            {
                Debug.LogWarning("����� ����������!");
                yield break;
            }
        }

        // �������� ����� ��������� ������
        yield return new WaitForSeconds(1.0f);
    }

    // ��������� ����������� public, ����� ����� ��� ����� � ����������
    public void NextDialogue()
    {
        if (isWaitingForInput)
        {
            Debug.Log("������ ������, ���������� ����� ������.");
            isWaitingForInput = false;
        }
    }
}