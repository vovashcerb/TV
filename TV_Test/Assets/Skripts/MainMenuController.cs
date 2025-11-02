using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private bool isSettingsOpen = false; 
    [Header("Settings GameObjects")]  

    [SerializeField] private GameObject settingsObject;
    [SerializeField] private GameObject mainCanvasObject;


    public void GoToSettings()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsObject.SetActive(isSettingsOpen);
        mainCanvasObject.SetActive(!isSettingsOpen);
    }

    //Просто запускаем сцену по ее названию через SceneManager
    //public void GoToStart(string CameraName)
    //{
    //    SceneManager.LoadScene(LVLSceneName);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}
