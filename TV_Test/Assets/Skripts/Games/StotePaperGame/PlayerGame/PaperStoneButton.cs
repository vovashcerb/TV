using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperStoneButton : MonoBehaviour
{
    public Types types;
    public int state;
    private Button button;
    private Image buttonImage;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        buttonImage = gameObject.GetComponent<Button>().GetComponent<Image>();
    }

    public void GetChoise_1()
    {
        gameObject.transform.parent.GetComponent<StonePaperGame>().ChooseObject_1(types);
    }

    public void ChooseButton(Types inType)
    {
        if (state == 0 && inType == types)
        {
            state = 1;
            buttonImage.color = StaticHolder.Instance.choosingColor;
        }
        else
        {
            state = 0;
            buttonImage.color = StaticHolder.Instance.unChuusingColor;
        }
    }
}
