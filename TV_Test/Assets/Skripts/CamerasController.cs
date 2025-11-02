using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public static CamerasController instance;
    [SerializeField] private List<GameObject> cameras = new List<GameObject>();
    [SerializeField] private Transform playerCamera;

    private void Start()
    {
        instance = this;
    }
    public void SetCamera(string cameraName)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            if(cameras[i].name == cameraName)
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }
        }

        switch(cameraName)
        {
            case "PlayerCamera":
                StaticHolder.Instance.isGameStarted = true; break;
        }
    }
    private void Update()
    {
        
    }
}
