using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // �w�i�̐����ύX���ꂽ��l��ς���
    const float MIN_POS = -31.0f;
    const float MAX_POS = 79.0f;
    const float BACKGROUND_SPEED = 0.2f;
    float speed;
    bool isStopBg = false;
    float delta = 0;
    float stopTime = 5.0f;
    float stopPos;
    bool flg = false;

    // �d��
    GameObject[] trainArray;

    // �w�i(�w�ȊO)
    GameObject[] backgroundArray;

    // �w�i(�w)
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
        // �w�i��x�������Ɉړ�������
        transform.position -= new Vector3(speed, 0, 0);

        if (transform.position.x <= MIN_POS)
        {
            // �w�i���Ō���Ɉړ�����
            transform.position = new Vector3(MAX_POS, transform.position.y, transform.position.z);

            if (gameObject == stationBackground)
            {
                //�@�w����ς���
                GameObject uiController = GameObject.Find("UIController");
                uiController.GetComponent<UIController>().changeStation();
               
            }
        }

        // �w�ɓ��������Ƃ��A�w�i�Ɠd�Ԃ��~�߂�
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
            // ��Ԏ��Ԃ��߂����Ƃ��A�w�i�A�d�Ԃ��ēx������
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
