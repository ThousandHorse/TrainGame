using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    const int STATION_SUM = 29;
    // テスト用
    //const int STATION_SUM = 2;

    GameObject trains;
    GameObject mainBackground;
    GameObject obstacleGenerator;

    List<GameObject> playerCountList = new List<GameObject>();

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI stationCountDownText;

    int stationCountDown;

    GameObject[] operationButttons;
    GameObject restartButton;
    int playerCount = 2;

    bool isStartedGame = false;

    float delta = 0;
    float countDownSpan = 1;
    int startCountDown = 3;

    void Start()
    {
        // プレイヤーの残人数をセット
        playerCountList.Add(GameObject.Find("PlayerCountImage1"));
        playerCountList.Add(GameObject.Find("PlayerCountImage2"));
        playerCountList.Add(GameObject.Find("PlayerCountImage3"));

        // ←、→、Jumpボタン
        operationButttons = GameObject.FindGameObjectsWithTag("OperationButtton");

        // Restartボタンを非表示
        restartButton = GameObject.FindGameObjectWithTag("RestartButton");
        restartButton.SetActive(false);

        // オブジェクトをそれぞれセットする
        trains = GameObject.FindGameObjectWithTag("Train");
        mainBackground = GameObject.FindGameObjectWithTag("MainBackground");
        obstacleGenerator = GameObject.Find("ObstacleGenerator");

        // 全駅数をセット
        stationCountDown = STATION_SUM;

        // カウントダウンを開始
        gameText.text = $"{startCountDown}";

    }

    void Update()
    {
        // ゲーム開始までカウントダウンをする
        StartCountDown();

        // 残駅数をテキスト表示
        stationCountDownText.text = $"残り{stationCountDown}駅";
    }

    // ゲーム開始までカウントダウンをする
    private void StartCountDown()
    {
        if (!isStartedGame)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.countDownSpan)
            {
                this.delta = 0;
                startCountDown--;

                if (startCountDown > 0)
                {
                    gameText.text = $"{startCountDown}";
                }
                else if (startCountDown == 0)
                {
                    gameText.text = $"START";

                    // 電車を動かす
                    trains.GetComponent<TrainController>().StartedGame();

                    // 背景を動かす
                    mainBackground.GetComponent<BackgroundController>().StartedGame();

                    // 障害物を生成させる
                    obstacleGenerator.GetComponent<ObstacleGenerator>().StartedGame();
                }
                else
                {
                    isStartedGame = true;
                    // ゲーム実行中はテキスト非表示
                    gameText.gameObject.SetActive(false);

                }

            }

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
            FinishGame($"GAME OVER\nRecord：{STATION_SUM - stationCountDown}/{STATION_SUM}駅達成！");
        }

    }

    public void RestartButtonEnter()
    {
        SceneManager.LoadScene("TrainGameScene");
    }

    public void FinishGame(string finishedGameText)
    {
        // ゲーム終了時にテキストを表示
        gameText.text = finishedGameText;
        gameText.color = new Color(1, 1, 0, 1);
        gameText.gameObject.SetActive(true);

        // ←、→、Jumpボタンを非表示
        foreach (var operationButtton in operationButttons)
        {
            operationButtton.SetActive(false);
        }

        // Restartボタンを表示
        restartButton.SetActive(true);

        // 障害物を出さないようにする
        GameObject obstacleGenerator = GameObject.Find("ObstacleGenerator");
        if (obstacleGenerator != null)
        {
            obstacleGenerator.GetComponent<ObstacleGenerator>().FinishGame();
        }

        // 障害物が既に生成されていた場合、破棄する
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obstacle in obstacles)
        {
            obstacle.GetComponent<ObstacleController>().DestroyObstacle();
        }

        // 電車を止める
        trains.GetComponent<TrainController>().StopTrain();

        // 背景の動きを止める
        mainBackground.GetComponent<BackgroundController>().StopBackGround(true);
    }

    // 駅が近くなったことを通知する
    public void NotifyStation(string stationName)
    {
        string removeBlank = stationName.Replace("　", "");
        gameText.text = $"まもなく{removeBlank}駅に到着します。";
        gameText.gameObject.SetActive(true);

    }
    
    // 画面真ん中のテキストを非表示にする
    public void HiddenText()
    {
        gameText.gameObject.SetActive(false);
    }

    // 残駅数を減らす
    public void StationCountDown()
    {
        stationCountDown--;
    }

    
}
