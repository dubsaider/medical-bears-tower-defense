using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DialogueManager dialogueManager = (DialogueManager)target;

        // Отображение полей dialogueText, leftCharacterImage, rightCharacterImage, backgroundImage и canvas
        dialogueManager.dialogueText = (TMPro.TextMeshProUGUI)EditorGUILayout.ObjectField("Dialogue Text", dialogueManager.dialogueText, typeof(TMPro.TextMeshProUGUI), true);
        dialogueManager.leftCharacterImage = (Image)EditorGUILayout.ObjectField("Left Character Image", dialogueManager.leftCharacterImage, typeof(Image), true);
        dialogueManager.rightCharacterImage = (Image)EditorGUILayout.ObjectField("Right Character Image", dialogueManager.rightCharacterImage, typeof(Image), true);
        dialogueManager.backgroundImage = (Image)EditorGUILayout.ObjectField("Background Image", dialogueManager.backgroundImage, typeof(Image), true);
        dialogueManager.canvas = (Canvas)EditorGUILayout.ObjectField("Canvas", dialogueManager.canvas, typeof(Canvas), true);

        // Отображение списка персонажей
        EditorGUILayout.LabelField("Characters", EditorStyles.boldLabel);
        for (int i = 0; i < dialogueManager.characters.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Character " + dialogueManager.characters[i].characterNumber, EditorStyles.boldLabel);
            dialogueManager.characters[i].characterNumber = EditorGUILayout.IntField("Character Number", dialogueManager.characters[i].characterNumber);
            dialogueManager.characters[i].characterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite", dialogueManager.characters[i].characterSprite, typeof(Sprite), false);
            EditorGUILayout.EndVertical();
        }

        // Добавление нового персонажа
        if (GUILayout.Button("Add Character"))
        {
            dialogueManager.characters.Add(new Character());
        }

        // Отображение списка диалоговых реплик
        EditorGUILayout.LabelField("Dialogue Lines", EditorStyles.boldLabel);
        for (int i = 0; i < dialogueManager.dialogueLines.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Dialogue Line " + (i + 1), EditorStyles.boldLabel);
            dialogueManager.dialogueLines[i].characterNumber = EditorGUILayout.IntField("Character Number", dialogueManager.dialogueLines[i].characterNumber);
            dialogueManager.dialogueLines[i].dialogueText = EditorGUILayout.TextField("Dialogue Text", dialogueManager.dialogueLines[i].dialogueText);

            // Кнопка для удаления Dialogue Line
            if (GUILayout.Button("Remove"))
            {
                dialogueManager.dialogueLines.RemoveAt(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        // Добавление новой реплики
        if (GUILayout.Button("Add Dialogue Line"))
        {
            dialogueManager.dialogueLines.Add(new DialogueLine());
        }

        // Сохранение изменений
        if (GUI.changed)
        {
            EditorUtility.SetDirty(dialogueManager);
        }
    }
}