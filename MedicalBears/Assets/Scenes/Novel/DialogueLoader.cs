using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAnimator textAnimator;
    public string fileName = "dialogue";

    void Start()
    {
        if (textAnimator != null)
        {
            string[] dialogues = LoadDialogues(fileName);
            textAnimator.SetDialogues(dialogues);
        }
        else
        {
            Debug.LogError("TextAnimator не назначен!");
        }
    }

    private string[] LoadDialogues(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            Debug.LogError("Файл не найден: " + fileName);
            return null;
        }

        return textAsset.text.Split('\n');
    }
}