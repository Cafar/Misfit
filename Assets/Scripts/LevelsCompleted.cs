using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsCompleted : MonoBehaviour
{
    public static LevelsCompleted instance;
    
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    public void PlayerPrefsSetBool(string levelName)
    {
        PlayerPrefs.SetInt(levelName, 0);
        Debug.Log("Has acabado el nivel " + levelName);
    }

    public bool PlayerPrefsGetBool(string levelName)
    {
        Debug.Log("El nivel " + levelName + (PlayerPrefs.HasKey(levelName) ? "" : " no" ) + " existe.");
        return PlayerPrefs.HasKey(levelName);
    }
}
