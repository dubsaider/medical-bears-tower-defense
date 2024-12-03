using UnityEngine;
using System.Collections.Generic;

namespace Code.Entities
{
    [CreateAssetMenu(menuName = "MedicalBears/DialogueSession")]
    public class DialogueSession : ScriptableObject
    {
        public List<Character> characters = new List<Character>();
        public List<DialogueLine> dialogueLines = new List<DialogueLine>();
        public Sprite background; // Добавляем поле для фона
    }
}