using UnityEngine;

public class DialogueInitializer : MonoBehaviour
{
    public TextAnimator textAnimator;
    public string[][] dialogues = new string[][]
    {
        new string[] { "�������� 1: ������!", "�������� 1: ��� ����?" },
        new string[] { "�������� 2: ������!", "�������� 2: �� ������, �������!" }
    };

    public Sprite[] characterSprites; // ������� ��� ������� ���������

    void Start()
    {
        if (textAnimator != null)
        {
            textAnimator.SetDialoguesAndSprites(dialogues, characterSprites);
        }
        else
        {
            Debug.LogError("TextAnimator �� ��������!");
        }
    }
}