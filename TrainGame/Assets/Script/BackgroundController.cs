using UnityEngine;
using TMPro;

public class BackgroundController : MonoBehaviour
{
    // 背景の数が変更されたら値を変える
    const float BACKGROUND_SPEED = 0.02f;
    float speed;
    bool isStopBg = false;
    bool isFinishedGame = false;
    float delta = 0;
    float stopTime = 3.0f;
    // ゲーム開始時の位置
    float defaultPosX;
    // 停車位置
    float stopPosX;
    // 折り返し位置
    float firstBgPosX;

    public TextMeshProUGUI stationName;

    string[] stations = new string[29]
        {
            "大　崎","品　川","田　町","浜松町","新　橋","有楽町",
            "東　京","神　田","秋葉原","御徒町","上　野","鶯　谷",
            "日暮里","西日暮里","田　端","駒　込","巣　鴨","大　塚",
            "池　袋","目　白","高田馬場","新大久保","新　宿","代々木",
            "原　宿","渋　谷","恵比寿","目　黒","五反田"
        };

    //string[] stations = new string[3]
    //    {
    //        "大　崎","品　川","田　町"
    //    };

    int stationCount = 0;


    // 電車
    GameObject[] trains;

    // 先頭の背景
    GameObject firstBackground;

    // 最後尾の背景
    GameObject lastBackground;

    // 駅の背景
    GameObject stationBackground;

    // UI操作
    GameObject uiController;


    void Start()
    {
        speed = BACKGROUND_SPEED;
        trains = GameObject.FindGameObjectsWithTag("Train");
        firstBackground = GameObject.FindGameObjectWithTag("FirstBackground");
        lastBackground = GameObject.FindGameObjectWithTag("LastBackground");
        stationBackground = GameObject.FindGameObjectWithTag("MainBackground");

        defaultPosX = transform.position.x;
        stopPosX = defaultPosX;

        firstBgPosX = defaultPosX - firstBackground.transform.position.x;

        // 駅名をセット
        stationName.color = new Color(0, 0, 0, 1);
        if (stations.Length >= 1)
        {
            stationName.text = stations[stations.Length - 1];
        }

        uiController = GameObject.Find("UIController");

    }

    void Update()
    {
        if (!isFinishedGame)
        {
            // 背景をx軸方向に移動させる
            transform.position -= new Vector3(speed, 0, 0);

            if (lastBackground.transform.position.x <= defaultPosX)
            {
                // 背景を最後尾に移動する
                transform.position = new Vector3(firstBgPosX, transform.position.y, transform.position.z);

                if (gameObject == stationBackground)
                {
                    if (stationCount < stations.Length)
                    {
                        // 駅名を変える
                        stationName.text = stations[stationCount];
                        stationCount++;

                        // 駅が近づいたことを知らせる
                        uiController.GetComponent<UIController>().notifyStation(stationName.text);
                    }

                    // 停車位置を再設定
                    stopPosX = defaultPosX;

                }
                
            }


            // 駅に到着したとき
            if (transform.position.x <= stopPosX)
            {
                // 背景を止める
                StopBackGround(false);

                // 電車を止める
                foreach (var train in trains)
                {
                    train.GetComponent<TrainController>().StopTrain();
                }

                // テキストを非表示にする
                uiController.GetComponent<UIController>().hiddenText();

                // 最後の駅に到着した場合、ゲームクリアを表示する
                if (stationCount == stations.Length)
                {
                    GameObject uiController = GameObject.Find("UIController");
                    uiController.GetComponent<UIController>().FinishGame("GAME CLEAR");
                    
                    isFinishedGame = true;
                    isStopBg = false;
                }

                if (isStopBg)
                {
                    this.delta += Time.deltaTime;
                    // 停車時間を過ぎたとき、背景、電車を再度動かす
                    if (this.delta >= stopTime)
                    {
                        this.delta = 0;
                        // 当該条件処理をループしないように退避させる
                        stopPosX = -10000;

                        RunBackGround();

                        foreach (var train in trains)
                        {
                            train.GetComponent<TrainController>().RunTrain();
                        }
                    }
                }
            }
        }

    }

    public void StopBackGround(bool isFinishedGame)
    {
        // ゲーム終了時、電車が動かないようにフラグを立てる
        this.isFinishedGame = isFinishedGame;
        speed = 0;
        isStopBg = true;
        
    }

    public void RunBackGround()
    {
        speed = BACKGROUND_SPEED;
        isStopBg = false;
    }

}
