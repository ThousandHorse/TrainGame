using UnityEngine;
using TMPro;

public class BackgroundController : MonoBehaviour
{
    const float BACKGROUND_SPEED = 0.02f;
    const float STOP_TIME = 3.0f;

    float speed;
    bool isStopBg = false;
    bool isFinishedGame = false;
    float delta = 0;

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

    // �ȈՔ�(�e�X�g�p)
    //string[] stations = new string[2]
    //    {
    //        "��@��","�i�@��"
    //    };

    int stationCount = 0;
    // �c��w��
    int stationCountDown;

    // �d��
    GameObject trains;

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
        trains = GameObject.FindGameObjectWithTag("Train");
        firstBackground = GameObject.FindGameObjectWithTag("FirstBackground");
        lastBackground = GameObject.FindGameObjectWithTag("LastBackground");
        stationBackground = GameObject.FindGameObjectWithTag("MainBackground");

        uiController = GameObject.Find("UIController");

        defaultPosX = transform.position.x;
        stopPosX = defaultPosX;

        firstBgPosX = defaultPosX - firstBackground.transform.position.x;

        // �w�����Z�b�g
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
                        uiController.GetComponent<UIController>().NotifyStation(stationName.text);
                    }

                    // ��Ԉʒu���Đݒ�
                    stopPosX = defaultPosX;

                }

            }


            // �w�ɓ��������Ƃ�
            if (!isStopBg && transform.position.x <= stopPosX)
            {
                // �w�i���~�߂�
                StopBackGround(false);

                // �d�Ԃ��~�߂�
                trains.GetComponent<TrainController>().StopTrain();

                // �A�i�E���X�̃e�L�X�g���\���ɂ���
                uiController.GetComponent<UIController>().HiddenText();

                //�c��w�������炷
                uiController.GetComponent<UIController>().StationCountDown();
            }
            else if (isStopBg)
            {
                // �Ō�̉w�ɓ��������ꍇ�A�Q�[���N���A��\������
                if (stationCount == stations.Length)
                {
                    uiController.GetComponent<UIController>().FinishGame("GAME CLEAR");

                    isFinishedGame = true;
                    isStopBg = false;
                }
                else
                {
                    this.delta += Time.deltaTime;

                    // ��Ԏ��Ԃ��߂����Ƃ�
                    if (this.delta >= STOP_TIME)
                    {
                        this.delta = 0;

                        // �w�i�A�d�Ԃ��ēx������
                        RunBackGround();
                        trains.GetComponent<TrainController>().RunTrain();

                        // ���Y�������������[�v���Ȃ��悤�ɑޔ�������
                        stopPosX = -10000;
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
