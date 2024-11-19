using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Game/Wave")]
public class Wave : ScriptableObject
{
    public float waitTime;
    public float duration;
    public int enemyCount;
}