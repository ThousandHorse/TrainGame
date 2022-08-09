using UnityEngine;
using TMPro;

public class BackgroundController : MonoBehaviour
{
    // �w�i�̐����ύX���ꂽ��l��ς���
    const float BACKGROUND_SPEED = 0.02f;
    float speed;
    bool isStopBg = false;
    bool isFinishedGame = false;
    float delta = 0;
    float stopTime = 3.0f;
    // �Q�[���J�n���̈ʒu
    float defaultPosX;
    // ��Ԉʒu
    float stopPosX;
    // �܂�Ԃ��ʒu
    float firstBgPosX;

    public TextMeshProUGUI stationName;

    string[] stations = new string[29]
        {
            "��@��","�i�@��","�c�@��","�l����","�V�@��","�L�y��",
            "���@��","�_�@�c","�H�t��","��k��","��@��","��@�J",
            "���闢","�����闢","�c�@�[","��@��","���@��","��@��",
            "�r�@��","�ځ@��","���c�n��","�V��v��","�V�@�h","��X��",
            "���@�h","�a�@�J","�b���","�ځ@��","�ܔ��c"
        };

    //string[] stations = new string[3]
    //    {
    //        "��@��","�i�@��","�c�@��"
    //    };

    int stationCount = 0;


    // �d��
    GameObject[] trains;

    // �擪�̔w�i
    GameObject firstBackground;

    // �Ō���̔w�i
    GameObject lastBackground;

    // �w�̔w�i
    GameObject stationBackground;

    // UI����
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

        // �w�����Z�b�g
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
            // �w�i��x�������Ɉړ�������
            transform.position -= new Vector3(speed, 0, 0);

            if (lastBackground.transform.position.x <= defaultPosX)
            {
                // �w�i���Ō���Ɉړ�����
                transform.position = new Vector3(firstBgPosX, transform.position.y, transform.position.z);

                if (gameObject == stationBackground)
                {
                    if (stationCount < stations.Length)
                    {
                        // �w����ς���
                        stationName.text = stations[stationCount];
                        stationCount++;

                        // �w���߂Â������Ƃ�m�点��
                        uiController.GetComponent<UIController>().notifyStation(stationName.text);
                    }

                    // ��Ԉʒu���Đݒ�
                    stopPosX = defaultPosX;

                }
                
            }


            // �w�ɓ��������Ƃ�
            if (transform.position.x <= stopPosX)
            {
                // �w�i���~�߂�
                StopBackGround(false);

                // �d�Ԃ��~�߂�
                foreach (var train in trains)
                {
                    train.GetComponent<TrainController>().StopTrain();
                }

                // �e�L�X�g���\���ɂ���
                uiController.GetComponent<UIController>().hiddenText();

                // �Ō�̉w�ɓ��������ꍇ�A�Q�[���N���A��\������
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
                    // ��Ԏ��Ԃ��߂����Ƃ��A�w�i�A�d�Ԃ��ēx������
                    if (this.delta >= stopTime)
                    {
                        this.delta = 0;
                        // ���Y�������������[�v���Ȃ��悤�ɑޔ�������
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
        // �Q�[���I�����A�d�Ԃ������Ȃ��悤�Ƀt���O�𗧂Ă�
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
