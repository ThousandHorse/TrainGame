using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // 背景の数が変更されたら値を変える
    const float MIN_POS = -31.0f;
    const float MAX_POS = 79.0f;
    const float BACKGROUND_SPEED = 0.2f;
    float speed;
    bool isStopBg = false;
    float delta = 0;
    float stopTime = 5.0f;
    float stopPos;
    bool flg = false;

    // 電車
    GameObject[] trainArray;

    // 背景(駅以外)
    GameObject[] backgroundArray;

    // 背景(駅)
    GameObject stationBackground;


    void Start()
    {
        speed = BACKGROUND_SPEED;
        trainArray = GameObject.FindGameObjectsWithTag("Train");
        backgroundArray = GameObject.FindGameObjectsWithTag("Background");
        stationBackground = GameObject.FindGameObjectWithTag("StationBackground");

        stopPos = transform.position.x;
    }

    void Update()
    {
        // 背景をx軸方向に移動させる
        transform.position -= new Vector3(speed, 0, 0);

        if (transform.position.x <= MIN_POS)
        {
            // 背景を最後尾に移動する
            transform.position = new Vector3(MAX_POS, transform.position.y, transform.position.z);

            if (gameObject == stationBackground)
            {
                //　駅名を変える
                GameObject uiController = GameObject.Find("UIController");
                uiController.GetComponent<UIController>().changeStation();
               
            }
        }

        // 駅に到着したとき、背景と電車を止める
        if (transform.position.x <= stopPos)
        {

            Debug.Log("Stop");
            StopBackGround();

            foreach (var train in trainArray)
            {
                train.GetComponent<TrainController>().StopTrain();
            }

        }

        if (isStopBg)
        {
            this.delta += Time.deltaTime;
            // 停車時間を過ぎたとき、背景、電車を再度動かす
            if (this.delta >= stopTime)
            {
                this.delta = 0;

                RunBackGround();

                foreach (var train in trainArray)
                {
                    train.GetComponent<TrainController>().RunTrain();
                }
            }
        }
    }

    public void StopBackGround()
    {
        speed = 0;
        isStopBg = true;
    }

    public void RunBackGround()
    {
        speed = BACKGROUND_SPEED;
        isStopBg = false;
    }

}
