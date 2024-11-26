using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAnimator textAnimator;
    public string[] fileNames = { "dialogue_character1", "dialogue_character2" };
    public Sprite[] characterSprites; // Спрайты для каждого персонажа

    void Start()
    {
        if (textAnimator != null)
        {
            string[][] dialogues = new string[fileNames.Length][];
            for (int i = 0; i < fileNames.Length; i++)
            {
                dialogues[i] = LoadDialogues(fileNames[i]);
            }
            textAnimator.SetDialoguesAndSprites(dialogues, characterSprites);
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