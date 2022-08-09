using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    const int SUM_TRAIN = 9;

    GameObject[] trainArray = new GameObject[SUM_TRAIN];
    List<GameObject> playerCountList = new List<GameObject>();

    public TextMeshProUGUI gameText;

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

    public void FinishGame(string finishedGameText)
    {
        // ゲーム終了時にテキストを表示
        gameText.text = finishedGameText;
        gameText.color = new Color(1, 0, 0, 1);
        gameText.gameObject.SetActive(true);

        // ←、→、Jumpボタンを非表示
        foreach (var operationButtton in operationButttons)
        {
            operationButtton.SetActive(false);
        }

        // Restartボタンを表示
        restartButton.SetActive(true);

        // 障害物を出さないようにする
        GameObject blockGenerator = GameObject.Find("BlockGenerator");
        if (blockGenerator != null)
        {
            blockGenerator.GetComponent<BlockGenerator>().FinishGame();
        }

        // 障害物が既に生成されていた場合、破棄する
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obstacle in obstacles)
        {
            obstacle.GetComponent<BlockController>().DestroyObstacle();
        }


        // 電車を止める
        for (int i = 0; i < SUM_TRAIN; i++)
        {
            string trainLocation = $"Train/Train{i}";
            trainArray[i] = GameObject.Find(trainLocation);
            trainArray[i].GetComponent<TrainController>().StopTrain();
        }

        // 背景の動きを止める
        GameObject mainBackground = GameObject.FindGameObjectWithTag("MainBackground");
        mainBackground.GetComponent<BackgroundController>().StopBackGround(true);
    }

    // 駅が近くなったことを通知する
    public void notifyStation(string stationName)
    {
        string removeBlank = stationName.Replace("　","");
        gameText.text = $"まもなく{removeBlank}駅に到着します。";
        gameText.gameObject.SetActive(true);

    }

    public void hiddenText()
    {
        gameText.gameObject.SetActive(false);
    }
}
