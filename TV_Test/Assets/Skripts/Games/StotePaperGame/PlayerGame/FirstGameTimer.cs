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
    private bool isCalculating;

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
    private float constStartTimer = 1;
    private float startTimer = 1;


    [Header("Счетчик раундов")]

    public int loose = 0;
    public int raunds = 0;
    public int wins = 0;
    public int hp = 5;

    [Header("Маленький экран")]
    [SerializeField] private TMP_Text raundsCount;
    [SerializeField] private Transform hpCount;


    void Update()
    {
        if (isCalculating) return;
        if (!StaticHolder.Instance.isGameStarted) return;
        startTimer -= Time.deltaTime;
        if (startTimer > 0) { timer.text = "Set..."; return; }
        time += Time.deltaTime;
        if (!isSecondRaund)
        {
            timer.text = "R1 Time: " + (porogy.x - (int)time).ToString();
        }
        else
        {
            timer.text = "R2 Time: " + (porogy.y - (int)time).ToString();
        }


        if (time > porogy.x && !isSecondRaund)
        {
            isCalculating = true;
            SetToSecondRaund();
        }
        else if (time > porogy.y && isSecondRaund)
        {
            //time = -100;
            int _isPlayerChoose = 0;
            for (int i = 0; i < StaticHolder.Instance.PaperStoneButtonSecond.Count; i++)
            {
                PaperStoneButton _choosingButton;
                if (StaticHolder.Instance.PaperStoneButtonSecond[i].state == 1)
                {
                    _choosingButton = StaticHolder.Instance.PaperStoneButtonSecond[i];
                   StartCoroutine(ChooseWinner(_choosingButton.types, gamePaper[Random.Range(0, 2)]));
                    _isPlayerChoose++;
                    break;
                }
            }
            if (_isPlayerChoose != 1) { PlayerLoose(); StartCoroutine(ResetGame()); }
        }
    }

    private IEnumerator ChooseWinner(Types player,Types enemy)
    {
        isCalculating = true;
        timer.text = "Fight";
        yield return new WaitForSeconds(2);
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

        Debug.Log(player + "  " + enemy);
       if (player == Types.scissors)
       {
            if(enemy == Types.scissors)
            {
                Debug.Log("Ничья");
                PlayerDraw();
            }
            else if(enemy == Types.Stone)
            {
                Debug.Log("Поражение");
                PlayerLoose();
            }
            else
            {
                Debug.Log("Победа");
                PlayerWin();
            }
       }

        if (player == Types.Stone)
        {
            if (enemy == Types.Stone)
            {
                Debug.Log("Ничья");
                PlayerDraw();
            }
            else if (enemy == Types.scissors)
            {
                Debug.Log("Победа");
                PlayerWin();
            }
            else
            {
                Debug.Log("Поражение");
                PlayerLoose();
            }
        }

        if (player == Types.Paper)
        {
            if (enemy == Types.Paper)
            {
                Debug.Log("Ничья");
                PlayerDraw();
            }
            else if (enemy == Types.Stone)
            {
                Debug.Log("Победа");
                PlayerWin();
            }
            else
            {
                Debug.Log("Поражение");
                PlayerLoose();
            }
        }
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2);
        ResetGame();
        raunds++;
        UpdateLittleEkran();
        startTimer = constStartTimer;
        time = 0;
        isSecondRaund = false;
        gamePaper_Left.ResetButtons();
        gamePaper_Right.ResetButtons();
        if(playerChoise.childCount > 1)  Destroy(playerChoise.GetChild(1).gameObject);
        if(playerChoise.childCount > 0)  Destroy(playerChoise.GetChild(0).gameObject);
        if(botChoise[0].childCount > 1) Destroy(botChoise[0].GetChild(1).gameObject);
        if(botChoise[0].childCount > 0) Destroy(botChoise[0].GetChild(0).gameObject);
        gamePaper.Clear();
        isCalculating = false;
    }

    public void SetToSecondRaund()
    {
        if (isSecondRaund) return;

            isCalculating = true;
            isSecondRaund = true;
            gamePaper_Left.ClearNotChoosing();
            gamePaper_Right.ClearNotChoosing();
        if (StaticHolder.Instance.PaperStoneButtonSecond.Count == 2)
        {
            time = 0;
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
            isCalculating = false;
        }
        else
        {
            PlayerLoose();
            StartCoroutine(ResetGame());
        }
    }

    private void PlayerLoose()
    {
        timer.text = "You Loose";
        loose++;
        if (loose != 0 && loose % 3 == 0)
        {
            hp--;
        }
    }

    private void PlayerWin()
    {
        timer.text = "You Win";
        wins++;
    }

    private void PlayerDraw()
    {
        timer.text = "Draw";
    }


    private void UpdateLittleEkran()
    {
        raundsCount.text = "Raunds: " + raunds;
        switch(hp)
        {
            case 4: Destroy(hpCount.GetChild(4).gameObject); break;
            case 3: Destroy(hpCount.GetChild(3).gameObject); break;
            case 2: Destroy(hpCount.GetChild(2).gameObject); break;
            case 1: Destroy(hpCount.GetChild(1).gameObject); break;
            case 0: Destroy(hpCount.GetChild(0).gameObject); break;
        }
        if(hp == 0)
        {
            CamerasController.instance.SetCamera("CatScene1_Camera");
            StaticHolder.Instance.isGameStarted = false;
            ResetGame();
        }
    }

    public void ButtonToSecondRaund()
    {
        
    }
    //до начала игры умы продумываем какие предметы ввыбрать и какие баффы выбрать
    //ХП - это лампочки, переделать Хп бар
    //Пустое поражение,сделать чтобы если не выбрал предмет то кпка быстрого старта не нажималась
    //Анимация для кнопки, для игрока
    //
}

