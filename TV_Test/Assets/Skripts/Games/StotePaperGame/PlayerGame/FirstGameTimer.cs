using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FirstGameTimer : MonoBehaviour
{
    [SerializeField] private float time = 0f;
    [SerializeField] private Vector2 porogy;
    [SerializeField] private bool isSecondRaund = false;

    public StonePaperGame gamePaper_Left;
    public StonePaperGame gamePaper_Right;

    [Header("Выбор робота")]
    private List<Types> gamePaper = new List<Types>();
    public List<Transform> botChoise = new List<Transform>();
    public GameObject buttonPreafab;
    [Header("Выбор игрока")]
    public Transform playerChoise;

    [Header("Что ресетнуть")]
    public GameObject RPC;
    public GameObject bigEcran;

    [Header("Таймер")]
    public TMP_Text timer;


    [Header("Счетчик раундов")]

    public int loose = 0;
    public int raunds = 0;
    public int wins = 0;


    void Update()
    {
        time += Time.deltaTime;
        if (!isSecondRaund)
        {
            timer.text = "R1 Time" + (porogy.x - (int)time).ToString();
        }
        else
        {
            timer.text = "R2 Time" + (porogy.y - (int)time).ToString();
        }
        if (time > porogy.x && !isSecondRaund)
        {
            time = 0f;
            isSecondRaund = true;
            //porogy.x = porogy.y;
            gamePaper_Left.ClearNotChoosing();
            gamePaper_Right.ClearNotChoosing();
            gamePaper.Add((Types)Random.Range(0, System.Enum.GetValues(typeof(Types)).Length));
            gamePaper.Add((Types)Random.Range(0, System.Enum.GetValues(typeof(Types)).Length));

            GameObject firstChoise = Instantiate(buttonPreafab, botChoise[0].position, botChoise[0].rotation, botChoise[0]);
            firstChoise.GetComponent<PaperStoneButton>().types = gamePaper[0];
            firstChoise.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = gamePaper[0].ToString();

            GameObject secondChoise = Instantiate(buttonPreafab, botChoise[0].position, botChoise[0].rotation, botChoise[0]);
            secondChoise.GetComponent<PaperStoneButton>().types = gamePaper[0];
            secondChoise.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = gamePaper[1].ToString();


            GameObject playerChoise_1 = Instantiate(buttonPreafab, playerChoise.position, playerChoise.rotation, playerChoise);
            playerChoise_1.GetComponent<PaperStoneButton>().types = StaticHolder.Instance.PaperStoneButtonSecond[0].types;
            playerChoise_1.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = StaticHolder.Instance.PaperStoneButtonSecond[0].types.ToString();

            GameObject playerChoise_2 = Instantiate(buttonPreafab, playerChoise.position, playerChoise.rotation, playerChoise);
            playerChoise_2.GetComponent<PaperStoneButton>().types = StaticHolder.Instance.PaperStoneButtonSecond[1].types;
            playerChoise_2.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = StaticHolder.Instance.PaperStoneButtonSecond[1].types.ToString();



        }
        else if (time > porogy.y && isSecondRaund)
        {
            for (int i = 0; i < StaticHolder.Instance.PaperStoneButtonSecond.Count; i++)
            {
                PaperStoneButton _choosingButton;
                if (StaticHolder.Instance.PaperStoneButtonSecond[i].state == 1)
                {
                    _choosingButton = StaticHolder.Instance.PaperStoneButtonSecond[i];
                    //StartCoroutine(ChooseWinner(_choosingButton.types, gamePaper[Random.Range(0,2)]));
                    ChooseWinner(_choosingButton.types, gamePaper[Random.Range(0, 2)]);
                    break;
                }
            }
        }
    }

    private void ChooseWinner(Types player,Types enemy)
    {

        for (int i = 0;i < botChoise[0].childCount; i++)
        {
            if(botChoise[0].GetChild(i).GetComponent<PaperStoneButton>().types == enemy)
            {
                Destroy(botChoise[0].GetChild(i).gameObject);
                break;
            }
        }

        for (int i = 0; i < playerChoise.childCount; i++)
        {
            if (playerChoise.GetChild(i).GetComponent<PaperStoneButton>().types == player)
            {
                Destroy(playerChoise.GetChild(i).gameObject);
                break;
            }
        }

        //////
        //////
        ///yield return new WaitForSeconds(2);
        //////
        //////

        Debug.Log(player + "  " + enemy);
       if (player == Types.scissors)
       {
            if(enemy == Types.scissors)
            {
                Debug.Log("Ничья");
            }
            else if(enemy == Types.Stone)
            {
                Debug.Log("Поражение");
                loose++;
            }
            else
            {
                Debug.Log("Победа");
                wins++;
            }
       }

        if (player == Types.Stone)
        {
            if (enemy == Types.Stone)
            {
                Debug.Log("Ничья");
            }
            else if (enemy == Types.scissors)
            {
                Debug.Log("Победа");
                wins++;
            }
            else
            {
                Debug.Log("Поражение");
                loose++;
            }
        }

        if (player == Types.Paper)
        {
            if (enemy == Types.Paper)
            {
                Debug.Log("Ничья");
            }
            else if (enemy == Types.Stone)
            {
                Debug.Log("Победа");
                wins++;
            }
            else
            {
                Debug.Log("Поражение");
                loose++;
            }
        }

        raunds++;
        ResetGame();
    }

    private void ResetGame()
    {
        time = 0;
        isSecondRaund = false;
        gamePaper_Left.ResetButtons();
        gamePaper_Right.ResetButtons();
        Destroy(playerChoise.GetChild(0).gameObject);
        Destroy(playerChoise.GetChild(1).gameObject);
        Destroy(botChoise[0].GetChild(0).gameObject);
        Destroy(botChoise[0].GetChild(1).gameObject);
        gamePaper.Clear();
        //Instantiate(RPC, RPC.transform.position, RPC.transform.rotation);
        //Instantiate(bigEcran, bigEcran.transform.position, bigEcran.transform.rotation);
        //Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
    }
}

