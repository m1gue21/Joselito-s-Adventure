﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance; 

    public string levelSelect, mainMenu;

    public GameObject pauseScreen;
    public bool isPaused;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            PauseUnPause();
        }
    }

    public void PauseUnPause()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;

        }else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

}
