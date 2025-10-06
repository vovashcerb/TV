using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private bool isSettingsOpen = false; // bool флаг видимости
    [Header("Settings GameObjects")]  // Это атрибут - заголовок, нужен только для того
                                      // чтобы отделять поля в эдиторе
    [SerializeField] private GameObject settingsObject; // Ссылка на объект настроек
    [SerializeField] private GameObject mainCanvasObject;// Ссылка на объект главного меню

    // Меняем значение флага isSettingsOpen, видимость объекта settingsObject
    // ставим на значение isSettingsOpen, а mainCanvasObject
    // на инвертированный isSettingsOpen
    public void GoToSettings()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsObject.SetActive(isSettingsOpen);
        mainCanvasObject.SetActive(!isSettingsOpen);
    }

    //Просто запускаем сцену по ее названию через SceneManager
    public void GoToStartLVL(string LVLSceneName)
    {
        SceneManager.LoadScene(LVLSceneName);
    }

    //Полностью выходим из игры
    public void QuitGame()
    {
        Application.Quit();
    }
}
