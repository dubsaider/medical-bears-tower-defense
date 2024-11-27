using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DialogueManager dialogueManager = (DialogueManager)target;

        // ����������� ������ ����������
        EditorGUILayout.LabelField("Characters", EditorStyles.boldLabel);
        for (int i = 0; i < dialogueManager.characters.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Character " + dialogueManager.characters[i].characterNumber, EditorStyles.boldLabel);
            dialogueManager.characters[i].characterNumber = EditorGUILayout.IntField("Character Number", dialogueManager.characters[i].characterNumber);
            dialogueManager.characters[i].characterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite", dialogueManager.characters[i].characterSprite, typeof(Sprite), false);
            EditorGUILayout.EndVertical();
        }

        // ����������� ������ ���������� ������
        EditorGUILayout.LabelField("Dialogue Lines", EditorStyles.boldLabel);
        for (int i = 0; i < dialogueManager.dialogueLines.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Dialogue Line " + (i + 1), EditorStyles.boldLabel);
            dialogueManager.dialogueLines[i].characterNumber = EditorGUILayout.IntField("Character Number", dialogueManager.dialogueLines[i].characterNumber);
            dialogueManager.dialogueLines[i].dialogueText = EditorGUILayout.TextField("Dialogue Text", dialogueManager.dialogueLines[i].dialogueText);
            EditorGUILayout.EndVertical();
        }

        // ���������� ����� �������
        if (GUILayout.Button("Add Dialogue Line"))
        {
            dialogueManager.dialogueLines.Add(new DialogueLine());
        }

        // ���������� ���������
        if (GUI.changed)
        {
            EditorUtility.SetDirty(dialogueManager);
        }
    }
}