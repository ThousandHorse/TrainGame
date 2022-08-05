using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // ”wŒi‚Ì”‚ª•ÏX‚³‚ê‚½‚ç’l‚ğ•Ï‚¦‚é
    const float MIN_POS = -31.0f;
    const float MAX_POS = 79.0f;
    const float BACKGROUND_SPEED = 0.2f;
    float speed;
    bool isStopBg = false;
    float delta = 0;
    float stopTime = 5.0f;
    float stopPos;
    bool flg = false;

    // “dÔ
    GameObject[] trainArray;

    // ”wŒi(‰wˆÈŠO)
    GameObject[] backgroundArray;

    // ”wŒi(‰w)
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
        // ”wŒi‚ğx²•ûŒü‚ÉˆÚ“®‚³‚¹‚é
        transform.position -= new Vector3(speed, 0, 0);

        if (transform.position.x <= MIN_POS)
        {
            // ”wŒi‚ğÅŒã”ö‚ÉˆÚ“®‚·‚é
            transform.position = new Vector3(MAX_POS, transform.position.y, transform.position.z);

            if (gameObject == stationBackground)
            {
                //@‰w–¼‚ğ•Ï‚¦‚é
                GameObject uiController = GameObject.Find("UIController");
                uiController.GetComponent<UIController>().changeStation();
               
            }
        }

        // ‰w‚É“’…‚µ‚½‚Æ‚«A”wŒi‚Æ“dÔ‚ğ~‚ß‚é
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
            // ’âÔŠÔ‚ğ‰ß‚¬‚½‚Æ‚«A”wŒiA“dÔ‚ğÄ“x“®‚©‚·
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
