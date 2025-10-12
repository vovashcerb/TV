using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonePaperGame : MonoBehaviour
{
    public List<PaperStoneButton> PaperStoneButton;
    private bool isSecondRound = false;
    public void ChooseObject_1(Types type)
    {
        if (!isSecondRound)
        {
            for (int i = 0; i < PaperStoneButton.Count; i++)
            {
                PaperStoneButton[i].ChooseButton(type);
            }
        }
        else
        {
            for (int i = 0; i < StaticHolder.Instance.PaperStoneButtonSecond.Count; i++)
            {
                StaticHolder.Instance.PaperStoneButtonSecond[i].ChooseButton(type);
            }
        }
    }

    public void ClearNotChoosing()
    {
        for (int i = 0; i < PaperStoneButton.Count; i++)
        {
            if(PaperStoneButton[i].state == 0)
            {
               PaperStoneButton[i].gameObject.SetActive(false);
            }
            else
            {
                PaperStoneButton[i].ChooseButton(PaperStoneButton[i].types);
                StaticHolder.Instance.PaperStoneButtonSecond.Add(PaperStoneButton[i]);
            }
        }
        isSecondRound = true;
    }
}

public enum Types
{
    Paper,
    Stone,
    scissors
}
