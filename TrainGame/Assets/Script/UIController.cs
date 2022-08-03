using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    List<GameObject> playerCountList = new List<GameObject>();
    GameObject[] trainArray = new GameObject[9];
    public TextMeshProUGUI finishedGameText;
    public TextMeshProUGUI stationName;
    public GameObject operationButtton;
    public GameObject restartButton;
    //float delta = 0;
    //float span = 1.0f;
    int stationCount = 0;
    int playerCount = 2;
    //bool isFinishedGame = false;

    string startStation = "五反田";
    string[] stationArray = new string[29]
        {
            "大　崎","品　川","田　町","浜松町","新　橋","有楽町",
            "東　京","神　田","秋葉原","御徒町","上　野","鶯　谷",
            "日暮里","西日暮里","田　端","駒　込","巣　鴨","大　塚",
            "池　袋","目　白","高田馬場","新大久保","新　宿","代々木",
            "原　宿","渋　谷","恵比寿","目　黒","五反田"
        };



    void Start()
    {
        // プレイヤーの残人数をセット
        playerCountList.Add(GameObject.Find("PlayerCountImage1"));
        playerCountList.Add(GameObject.Find("PlayerCountImage2"));
        playerCountList.Add(GameObject.Find("PlayerCountImage3"));

        // ←、→、Jumpボタン
        operationButtton = GameObject.Find("Canvas/Button");

        // Restartボタンを非表示
        restartButton = GameObject.Find("Canvas/RestartButton");
        restartButton.SetActive(false);

        // 駅名
        stationName.color = new Color(0, 0, 0, 1);
        stationName.text = startStation;

        // ゲーム実行中は非表示
        finishedGameText.gameObject.SetActive(false);


    }
    void Update()
    {
        //if (!isFinishedGame)
        //{
        //    this.delta += Time.deltaTime;
        //    if (this.delta > this.span)
        //    {
        //        this.delta = 0;
        //        stationCount++;
        //        if (stationCount < stationArray.Length)
        //        {
        //            stationName.text = stationArray[stationCount];
        //        }
        //        else
        //        {
        //            FinishGame("GAME CLEAR");
        //        }
        //    }
        //}

    }

    public void changeStation()
    {
        if (stationCount < stationArray.Length)
        {
            stationName.text = stationArray[stationCount];
            stationCount++;
        }
        else
        {
            FinishGame("GAME CLEAR");
            stationCount = 0;
        }
            
    }

    public void CollideWithBlock()
    {
        // プレイヤーのUIを削除する(3回まで)
        if (playerCount >= 0)
        {
            playerCountList[playerCount].SetActive(false);
            playerCount--;
        }
        // 4回目に衝突した場合
        else
        {
            FinishGame("GAME OVER");
        }

    }

    public void RestartButtonEnter()
    {
        SceneManager.LoadScene("TrainGameScene");
    }

    private void FinishGame(string uiText)
    {
        // ゲーム終了時にテキストを表示
        finishedGameText.text = uiText;
        finishedGameText.color = new Color(1, 0, 0, 1);
        finishedGameText.gameObject.SetActive(true);

        // ←、→、Jumpボタンを非表示
        operationButtton.SetActive(false);

        // Restartボタンを表示
        restartButton.SetActive(true);

        // 障害物を出さないようにする
        GameObject blockGenerator = GameObject.Find("BlockGenerator");
        if (blockGenerator != null)
        {
            blockGenerator.GetComponent<BlockGenerator>().FinishGame();
        }

        // 電車を止める
        trainArray[0] = GameObject.Find("Train/Train0");
        trainArray[1] = GameObject.Find("Train/Train1");
        trainArray[2] = GameObject.Find("Train/Train2");
        trainArray[3] = GameObject.Find("Train/Train3");
        trainArray[4] = GameObject.Find("Train/Train4");
        trainArray[5] = GameObject.Find("Train/Train5");
        trainArray[6] = GameObject.Find("Train/Train6");
        trainArray[7] = GameObject.Find("Train/Train7");
        trainArray[8] = GameObject.Find("Train/Train8");

        foreach (var train in trainArray)
        {
            train.GetComponent<TrainController>().StopTrain();
        }

        // 背景の動きを止める
        GameObject backgroundController = GameObject.Find("BackgroundController");
        backgroundController.GetComponent<BackgroundController>().StopBackGround();

        //isFinishedGame = true;

    }
}
