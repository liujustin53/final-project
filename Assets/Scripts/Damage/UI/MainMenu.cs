using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    public Text playtimeText;
    public Slider cameraSensitivity;

    public void Start()
    {
        SaveManager.RetrievePlayTime();
        TimeSpan time = TimeSpan.FromSeconds(SaveManager.Playtime);
        playtimeText.text = "Time Played:" + "\n" + time.ToString("hh':'mm':'ss");
        SaveManager.RetrieveControlParameters();
        cameraSensitivity.value = ControlParameters.lookSensitivity;
    }
    public void Update()
    {
        ControlParameters.lookSensitivity = cameraSensitivity.value;
    }
    public void StartGame()
    {
        SaveSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        SaveSettings();
        Application.Quit();
    }

    public void SaveSettings()
    {
        SaveManager.SaveControlParameters();
    }
}