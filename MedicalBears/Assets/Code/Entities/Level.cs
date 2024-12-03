using UnityEngine;

namespace Code.Entities
{
    [CreateAssetMenu(menuName = "MedicalBears/Level")]
    public class Level : ScriptableObject
    {
        public DialogueSession dialogueSession;
        public int mapIndex;
        public int startBalance;
        public Wave[] waves;
    }
}