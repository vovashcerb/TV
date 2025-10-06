using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private bool isSettingsOpen = false; // bool ���� ���������
    [Header("Settings GameObjects")]  // ��� ������� - ���������, ����� ������ ��� ����
                                      // ����� �������� ���� � �������
    [SerializeField] private GameObject settingsObject; // ������ �� ������ ��������
    [SerializeField] private GameObject mainCanvasObject;// ������ �� ������ �������� ����

    // ������ �������� ����� isSettingsOpen, ��������� ������� settingsObject
    // ������ �� �������� isSettingsOpen, � mainCanvasObject
    // �� ��������������� isSettingsOpen
    public void GoToSettings()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsObject.SetActive(isSettingsOpen);
        mainCanvasObject.SetActive(!isSettingsOpen);
    }

    //������ ��������� ����� �� �� �������� ����� SceneManager
    public void GoToStartLVL(string LVLSceneName)
    {
        SceneManager.LoadScene(LVLSceneName);
    }

    //��������� ������� �� ����
    public void QuitGame()
    {
        Application.Quit();
    }
}
