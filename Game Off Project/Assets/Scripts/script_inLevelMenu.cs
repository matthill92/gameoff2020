﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class script_inLevelMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public script_transition transition;
    public script_gravity[] planets;

    public Text level_complete_text;
    public GameObject next_level_button;

    int level_complete = 0;

    private void Update()
    {
        foreach (script_gravity planet in planets)
        {
            if (planet.complete == false && level_complete == 1)
                level_complete = 0;
        }
        if (level_complete == 1)
        {
            level_complete_text.text = "Complete";
            next_level_button.SetActive(true);
            openButton();
            level_complete = -1;
        }
        else if (level_complete == 0)
        {
            level_complete = 1;
        }
    }

    public void openButton()
    {
        pauseScreen.SetActive(true);

    }

    public void closeButton()
    {
        pauseScreen.SetActive(false);
    }

    public void mainMenuButton()
    {
        transition.changeLevel(0);
    }

    public void resetButton()
    {
        transition.changeLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevelButton()
    {
        transition.changeLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}