using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class SceneButtonPair
{
    public SceneAsset sceneAsset;
    public Button button;
}

public class SceneManagerScript : MonoBehaviour
{
    public List<SceneButtonPair> sceneButtonPairs;

    void Start()
    {
        foreach (var pair in sceneButtonPairs)
        {
            string sceneName = pair.sceneAsset.name;
            pair.button.onClick.AddListener(() => LoadScene(sceneName));
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}