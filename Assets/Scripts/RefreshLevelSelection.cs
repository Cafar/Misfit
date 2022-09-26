using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshLevelSelection : MonoBehaviour
{
    [SerializeField]
    ButtonSceneAndNames[] levelsButtonFromLevel2;

	void RefreshLevels ()
    {
        Debug.Log("Comprobando niveles.");
        for (int i = 0; i < levelsButtonFromLevel2.Length; i++)
        {
            levelsButtonFromLevel2[i].currentButton.interactable = LevelsCompleted.instance.PlayerPrefsGetBool(levelsButtonFromLevel2[i].sceneToLoadName);
        }
	}

    void OnEnable()
    {
        RefreshLevels();
    }
}
[System.Serializable]
public class ButtonSceneAndNames
{
    [SerializeField]
    public Button currentButton;
    [SerializeField]
    public string sceneToLoadName;
}