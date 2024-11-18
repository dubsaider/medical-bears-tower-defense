using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/Level")]
public class Level : ScriptableObject
{
    public int mapIndex;
    public Wave[] waves;
    public int startingBalance;
    public string additionalParameters;
}