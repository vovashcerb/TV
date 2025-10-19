
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerLifesBehaviour : MonoBehaviour
{
    public static PLayerLifesBehaviour instance { get; private set; }
    [SerializeField] private GameObject playerLifes;

    private void Start()
    {
        instance = this;
    }

    public void PlayerLifeUIKill()
    {

    }
}
