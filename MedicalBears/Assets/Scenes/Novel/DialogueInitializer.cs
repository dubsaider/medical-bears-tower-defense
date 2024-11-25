using UnityEngine;

public class DialogueInitializer : MonoBehaviour
{
    public TextAnimator textAnimator;
    public string dialoguesText = "Бу испугался не бойся. Я друг. \r\nЯ тебя не обижу. Иди сюда, иди ко мне, сядь рядом со мной. \r\nПосмотри мне в глаза. Ты видишь меня? Я тоже тебя вижу. \r\nДавай смотреть друг на друга до тех пор, пока наши глаза не устанут. \r\nТы не хочешь? Почему? Что-то не так?.";

    void Start()
    {
        if (textAnimator != null)
        {
            string[] dialogues = dialoguesText.Split('\n');
            textAnimator.SetDialogues(dialogues);
        }
        else
        {
            Debug.LogError("TextAnimator не назначен!");
        }
    }
}