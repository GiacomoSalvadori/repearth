﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    #region Properties
    public int actualScene;
    public int previousScene;
    public delegate void FlowChange();
    public FlowChange OnEndGame;
    public FlowChange OnLoading;
    private bool started;
    #endregion

    private void Awake()
    {
        SubscribeMeForEndgame();
        previousScene = -1;
        started = true;
        actualScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnChangeScene;
    }

    private void Start()
    {

    }

    private void OnChangeScene(Scene scene, LoadSceneMode sceneMode)
    {
        if (started) {
            started = false;
        } else {
            previousScene = actualScene;
        }
        actualScene = scene.buildIndex;
    }

    public void SubscribeMeForEndgame()
    {

    }

    private void EndGame()
    {
        if (OnEndGame != null) {
            OnEndGame();
        }
    }

    public void GoToScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName)) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void GoToScene(int sceneIndex)
    {
        if (sceneIndex > -1) {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }

    public IEnumerator LoadNextScene(int sceneIndex)
    {
        if (OnLoading != null) {
            OnLoading();
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void GoToNextScene()
    {
        int sceneNum = SceneManager.sceneCountInBuildSettings;
        if (actualScene < sceneNum - 1) {
            StartCoroutine(LoadNextScene(actualScene + 1));
        }
    }

    public void ReturnToLandpage()
    {
        GoToScene(0);
    }

    public bool IsNewGame()
    {
        Debug.Log("Prev scene " + previousScene + " " + (previousScene < 0));
        return previousScene < 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}