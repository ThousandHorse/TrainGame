using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    const int STATION_SUM = 29;
    const int STATION_SUM_TEST = 2;

    GameObject trains;
    GameObject mainBackground;

    List<GameObject> playerCountList = new List<GameObject>();

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI stationCountDownText;

    int stationCountDown;

    GameObject[] operationButttons;
    GameObject restartButton;
    int playerCount = 2;

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

        // ゲーム実行中は非表示
        gameText.gameObject.SetActive(false);

        trains = GameObject.FindGameObjectWithTag("Train");
        mainBackground = GameObject.FindGameObjectWithTag("MainBackground");


        stationCountDown = STATION_SUM_TEST + 1;

    }

    void Update()
    {
        // 残駅数をテキスト表示
        stationCountDownText.text = $"残り{stationCountDown}駅";
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
            FinishGame($"GAME OVER\n記録：{STATION_SUM_TEST - stationCountDown}/{STATION_SUM_TEST}駅達成！");
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

    public void HiddenText()
    {
        gameText.gameObject.SetActive(false);
    }

    public void StationCountDown()
    {
        // 残駅数を減らす
        stationCountDown--;
    }
}
