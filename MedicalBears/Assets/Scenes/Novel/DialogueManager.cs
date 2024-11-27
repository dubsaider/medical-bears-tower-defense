using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Image leftCharacterImage;
    public Image rightCharacterImage;
    public Image backgroundImage; // ������� �����������
    public Canvas canvas; // ������

    [SerializeField]
    public List<Character> characters = new List<Character>();

    [SerializeField]
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();

    private int currentDialogueIndex = 0;
    private string currentDialogue;
    private int currentCharIndex = 0;
    private float typeSpeed = 0.05f; // �������� �������� ������ (� �������� �� ������)
    private float backgroundMoveSpeed = 2f; // �������� �������� ����

    void Start()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (currentDialogueIndex < dialogueLines.Count)
        {
            currentDialogue = dialogueLines[currentDialogueIndex].dialogueText;
            currentCharIndex = 0;
            StopAllCoroutines(); // ������������� ���������� ��������
            StartCoroutine(TypeText());

            // ��������� ������� ���������
            int characterNumber = dialogueLines[currentDialogueIndex].characterNumber;
            Character character = characters.Find(c => c.characterNumber == characterNumber);
            if (character != null && character.characterSprite != null)
            {
                if (characterNumber % 2 == 0)
                {
                    leftCharacterImage.sprite = character.characterSprite;
                    leftCharacterImage.color = Color.white; // ���������� ������ ��������������
                    rightCharacterImage.sprite = null;
                    rightCharacterImage.color = new Color(1, 1, 1, 0); // ������ ��������� ����������
                    StartCoroutine(MoveBackground(true)); // ������� ��� �����
                }
                else
                {
                    rightCharacterImage.sprite = character.characterSprite;
                    rightCharacterImage.color = Color.white; // ���������� ������ ��������������
                    leftCharacterImage.sprite = null;
                    leftCharacterImage.color = new Color(1, 1, 1, 0); // ������ ��������� ����������
                    StartCoroutine(MoveBackground(false)); // ������� ��� ������
                }
            }
        }
        else
        {
            Debug.Log("������ ��������");
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

    IEnumerator MoveBackground(bool moveLeft)
    {
        RectTransform backgroundRect = backgroundImage.GetComponent<RectTransform>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 targetPosition = moveLeft ? new Vector2(-canvasRect.rect.width / 2, backgroundRect.anchoredPosition.y) : new Vector2(canvasRect.rect.width / 2, backgroundRect.anchoredPosition.y);

        while (Vector2.Distance(backgroundRect.anchoredPosition, targetPosition) > 0.1f)
        {
            backgroundRect.anchoredPosition = Vector2.Lerp(backgroundRect.anchoredPosition, targetPosition, backgroundMoveSpeed * Time.deltaTime);
            yield return null;
        }

        backgroundRect.anchoredPosition = targetPosition;
    }

    // ����� ��� ������ ��������� ����� ������� ����� ������ ��� �������
    public void OnClick_NextPart()
    {
        if (currentCharIndex < currentDialogue.Length)
        {
            currentCharIndex = currentDialogue.Length; // ������ ������� ���������� ����� ������
            dialogueText.text = currentDialogue; // ���������� ������ ����� �������
        }
        else if (currentDialogueIndex < dialogueLines.Count - 1)
        {
            currentDialogueIndex++;
            StartDialogue();
        }
        else
        {
            Debug.Log("������ ��������");
        }
    }
}