using UnityEngine;

namespace Code.Entities
{
    [CreateAssetMenu(menuName = "MedicalBears/Level")]
    public class Level : ScriptableObject
    {
        public int mapIndex;
        public float startBalance;
        public Wave[] waves;
    }
}