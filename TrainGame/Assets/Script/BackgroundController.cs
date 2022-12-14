using UnityEngine;
using TMPro;

public class BackgroundController : MonoBehaviour
{
    const float BACKGROUND_SPEED = 0.02f;
    const float STOP_TIME = 3.0f;

    float speed;
    bool isStopBg = false;
    bool isFinishedGame = false;
    bool isStartedGame = false;
    float delta = 0;

    // Q[JnΜΚu
    float defaultPosX;
    // βΤΚu
    float stopPosX;
    // άθΤ΅Κu
    float firstBgPosX;

    public TextMeshProUGUI stationName;

    string[] stations = new string[29]
        {
            "ε@θ","i@μ","c@¬","lΌ¬","V@΄","Ly¬",
            "@","_@c","Ht΄","δk¬","γ@μ","ις@J",
            "ϊι’","Όϊι’","c@[","ξ@","@","ε@Λ",
            "r@ά","Ϊ@","cnκ","VεvΫ","V@h","γXΨ",
            "΄@h","a@J","bδυ","Ϊ@","ά½c"
        };

    // ΘΥΕ(eXgp)
    //string[] stations = new string[2]
    //    {
    //        "ε@θ","i@μ"
    //    };

    int stationCount = 0;
    // cθw
    int stationCountDown;

    // dΤ
    GameObject trains;

    // ζͺΜwi
    GameObject firstBackground;

    // ΕγφΜwi
    GameObject lastBackground;

    // wΜwi
    GameObject stationBackground;

    // UIμ
    GameObject uiController;


    void Start()
    {
        speed = BACKGROUND_SPEED;
        trains = GameObject.FindGameObjectWithTag("Train");
        firstBackground = GameObject.FindGameObjectWithTag("FirstBackground");
        lastBackground = GameObject.FindGameObjectWithTag("LastBackground");
        stationBackground = GameObject.FindGameObjectWithTag("MainBackground");

        uiController = GameObject.Find("UIController");

        defaultPosX = transform.position.x;
        stopPosX = -10000;

        firstBgPosX = defaultPosX - firstBackground.transform.position.x;

        // wΌπZbg
        stationName.color = new Color(0, 0, 0, 1);
        if (stations.Length >= 1)
        {
            stationName.text = stations[stations.Length - 1];
            stationCountDown = stations.Length;
        }

        uiController = GameObject.Find("UIController");
    }

    void Update()
    {
        if (isStartedGame && !isFinishedGame)
        {
            // wiπx²ϋόΙΪ?³Ήι
            transform.position -= new Vector3(speed, 0, 0);

            if (lastBackground.transform.position.x <= defaultPosX)
            {
                // wiπΕγφΙΪ?·ι
                transform.position = new Vector3(firstBgPosX, transform.position.y, transform.position.z);

                if (gameObject == stationBackground)
                {
                    if (stationCount < stations.Length)
                    {
                        // wΌπΟ¦ι
                        stationName.text = stations[stationCount];
                        stationCount++;

                        // wͺίΓ’½±ΖπmηΉι
                        uiController.GetComponent<UIController>().NotifyStation(stationName.text);
                    }

                    // βΤΚuπΔέθ
                    stopPosX = defaultPosX;

                }

            }


            // wΙ΅½Ζ«
            if (!isStopBg && transform.position.x <= stopPosX)
            {
                // wiπ~ίι
                StopBackGround(false);

                // dΤπ~ίι
                trains.GetComponent<TrainController>().StopTrain();

                // AiEXΜeLXgπρ\¦Ι·ι
                uiController.GetComponent<UIController>().HiddenText();

                //cθwπΈη·
                uiController.GetComponent<UIController>().StationCountDown();
            }
            else if (isStopBg)
            {
                // ΕγΜwΙ΅½κAQ[NAπ\¦·ι
                if (stationCount == stations.Length)
                {
                    uiController.GetComponent<UIController>().FinishGame("GAME CLEAR");

                    isFinishedGame = true;
                    isStopBg = false;
                }
                else
                {
                    this.delta += Time.deltaTime;

                    // βΤΤπί¬½Ζ«
                    if (this.delta >= STOP_TIME)
                    {
                        this.delta = 0;

                        // wiAdΤπΔx?©·
                        RunBackGround();
                        trains.GetComponent<TrainController>().RunTrain();

                        // Yππ[v΅Θ’ζ€Ιήπ³Ήι
                        stopPosX = -10000;
                    }
                }
            }
        }

    }

    public void StopBackGround(bool isFinishedGame)
    {
        // Q[IΉAdΤͺ?©Θ’ζ€ΙtOπ§Δι
        this.isFinishedGame = isFinishedGame;
        speed = 0;
        isStopBg = true;

    }

    public void RunBackGround()
    {
        speed = BACKGROUND_SPEED;
        isStopBg = false;
    }

    public void StartedGame()
    {
        isStartedGame = true;
    }

}
