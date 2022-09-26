using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class LevelBehaviour : MonoBehaviour {

    public Fade fade;
    [SerializeField]
    PlayerState player;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Cancel"))
        StartCoroutine("LoadLevelCo", 1);
    }

    public void ReloadCurrentSceneBySquare()
    {
        AudioSource audioso = FindObjectOfType<AudioSource>();
        StartCoroutine (AudioFadeOut.FadeOut (audioso, 2f));
        StartCoroutine (Camera.main.GetComponent<ScreenTransitionImageEffect>().FadeSquare());
        StartCoroutine (LoadLevelSquare(SceneManager.GetActiveScene().buildIndex));

    }

    public void ReloadCurrentScene()
    {
        AudioSource audioso = FindObjectOfType<AudioSource>();
        StartCoroutine (AudioFadeOut.FadeOut (audioso, 2f));
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadToSplash()
    {
        AudioSource audioso = FindObjectOfType<AudioSource>();
        StartCoroutine (AudioFadeOut.FadeOut (audioso, 2f));    
        LoadScene(0);

    }

    public void LoadScene(int sceneToLoad)
    {
        StartCoroutine("LoadLevelCo", sceneToLoad);
    }

    IEnumerator LoadLevelSquare(int newLevel)
    {
        yield return new WaitForSeconds(2);
        LoadSceneCut(newLevel);
    }

    public void LoadSceneCut(int sceneToLoad)
    {
        Debug.Log("LastEffectsInCutIfNecessary");
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator LoadLevelCo(int newLevel)
    {
        fade.FadeMeIn();
        if(player != null)
            player.CanMove = false;
        yield return new WaitForSeconds(2);
        LoadSceneCut(newLevel);
    }

    public void NextScene()
    {
        int sceneToLoad = (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings) ? SceneManager.GetActiveScene().buildIndex + 1 : 0;
        Debug.Log(sceneToLoad);
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;



    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        fade.FadeMeOut();
     
    }
}
