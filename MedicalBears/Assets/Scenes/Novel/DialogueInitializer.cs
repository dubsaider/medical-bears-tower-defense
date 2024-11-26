using UnityEngine;

public class DialogueInitializer : MonoBehaviour
{
    public TextAnimator textAnimator;
    public string[][] dialogues = new string[][]
    {
        new string[] { "Персонаж 1: Привет!", "Персонаж 1: Как дела?" },
        new string[] { "Персонаж 2: Привет!", "Персонаж 2: Всё хорошо, спасибо!" }
    };

    public Sprite[] characterSprites; // Спрайты для каждого персонажа

    void Start()
    {
        if (textAnimator != null)
        {
            textAnimator.SetDialoguesAndSprites(dialogues, characterSprites);
        }
        else
        {
            Debug.LogError("TextAnimator не назначен!");
        }
    }
}