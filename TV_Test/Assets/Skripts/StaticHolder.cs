using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHolder : MonoBehaviour
{
    public static StaticHolder Instance;
    public List<PaperStoneButton> PaperStoneButtonSecond;
    private void Start()
    {
        Instance = this;
    }
    public Color choosingColor;
    public Color unChuusingColor;

    public int Game_1_Raund;

    public bool isGameStarted = false;
}
