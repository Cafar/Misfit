using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDetector : MonoBehaviour
{
    [SerializeField]
    LevelBehaviour levelBehaviour;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D player) {
		if (player.GetComponent<PlayerState>())
        {
            LevelsCompleted.instance.PlayerPrefsSetBool(levelBehaviour.GetCurrentSceneName());
            levelBehaviour.NextScene();
        }
	}
}
