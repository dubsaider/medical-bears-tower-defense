using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Code.Entities;

public class DialogueSessionCreatorEditor : EditorWindow
{
    private List<Character> characters = new List<Character>();
    private List<DialogueLine> dialogueLines = new List<DialogueLine>();
    private string sessionName = "NewDialogueSession";
    private Sprite background; // Добавляем поле для фона

    [MenuItem("Window/Dialogue Session Creator")]
    public static void ShowWindow()
    {
        GetWindow<DialogueSessionCreatorEditor>("Dialogue Session Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Dialogue Session", EditorStyles.boldLabel);

        sessionName = EditorGUILayout.TextField("Session Name", sessionName);

        // Поле для выбора фона
        background = (Sprite)EditorGUILayout.ObjectField("Background", background, typeof(Sprite), false);

        // Отображение списка персонажей
        GUILayout.Label("Characters", EditorStyles.boldLabel);
        for (int i = 0; i < characters.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Character " + characters[i].characterNumber, EditorStyles.boldLabel);
            characters[i].characterNumber = EditorGUILayout.IntField("Character Number", characters[i].characterNumber);
            characters[i].characterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite", characters[i].characterSprite, typeof(Sprite), false);
            if (GUILayout.Button("Remove Character"))
            {
                characters.RemoveAt(i);
            }
            EditorGUILayout.EndVertical();
        }

        // Добавление нового персонажа
        if (GUILayout.Button("Add Character"))
        {
            characters.Add(new Character());
        }

        // Отображение списка диалоговых реплик
        GUILayout.Label("Dialogue Lines", EditorStyles.boldLabel);
        for (int i = 0; i < dialogueLines.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Dialogue Line " + (i + 1), EditorStyles.boldLabel);

            // Кнопки для перемещения реплики вверх и вниз
            EditorGUILayout.BeginHorizontal();
            if (i > 0 && GUILayout.Button("↑"))
            {
                SwapDialogueLines(i, i - 1);
            }
            if (i < dialogueLines.Count - 1 && GUILayout.Button("↓"))
            {
                SwapDialogueLines(i, i + 1);
            }
            EditorGUILayout.EndHorizontal();

            dialogueLines[i].characterNumber = EditorGUILayout.IntField("Character Number", dialogueLines[i].characterNumber);
            dialogueLines[i].dialogueText = EditorGUILayout.TextField("Dialogue Text", dialogueLines[i].dialogueText);

            // Проверка на количество строк
            int lineCount = dialogueLines[i].dialogueText.Split('\n').Length;
            if (lineCount > 50)
            {
                EditorGUILayout.HelpBox("Dialogue line exceeds 50 lines!", MessageType.Warning);
            }

            if (GUILayout.Button("Remove Dialogue Line"))
            {
                dialogueLines.RemoveAt(i);
            }
            EditorGUILayout.EndVertical();
        }

        // Добавление новой реплики
        if (GUILayout.Button("Add Dialogue Line"))
        {
            dialogueLines.Add(new DialogueLine());
        }

        // Сохранение нового DialogueSession
        if (GUILayout.Button("Save Dialogue Session"))
        {
            SaveDialogueSession();
        }
    }

    private void SwapDialogueLines(int indexA, int indexB)
    {
        DialogueLine temp = dialogueLines[indexA];
        dialogueLines[indexA] = dialogueLines[indexB];
        dialogueLines[indexB] = temp;
    }

    private void SaveDialogueSession()
    {
        DialogueSession newSession = ScriptableObject.CreateInstance<DialogueSession>();
        newSession.characters = new List<Character>(characters);
        newSession.dialogueLines = new List<DialogueLine>(dialogueLines);
        newSession.background = background; // Сохраняем фон

        string path = EditorUtility.SaveFilePanelInProject("Save Dialogue Session", sessionName, "asset", "Please enter a file name to save the dialogue session to");

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newSession, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newSession;
        }
    }
}