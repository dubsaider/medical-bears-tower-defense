using UnityEngine;

public class DialogueInitializer : MonoBehaviour
{
    public TextAnimator textAnimator;
    public string dialoguesText = "�� ��������� �� �����. � ����. \r\n� ���� �� �����. ��� ����, ��� �� ���, ���� ����� �� ����. \r\n�������� ��� � �����. �� ������ ����? � ���� ���� ����. \r\n����� �������� ���� �� ����� �� ��� ���, ���� ���� ����� �� �������. \r\n�� �� ������? ������? ���-�� �� ���?.";

    void Start()
    {
        if (textAnimator != null)
        {
            string[] dialogues = dialoguesText.Split('\n');
            textAnimator.SetDialogues(dialogues);
        }
        else
        {
            Debug.LogError("TextAnimator �� ��������!");
        }
    }
}