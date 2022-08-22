using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    const int STATION_SUM = 29;
    // �e�X�g�p
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
        // �v���C���[�̎c�l�����Z�b�g
        playerCountList.Add(GameObject.Find("PlayerCountImage1"));
        playerCountList.Add(GameObject.Find("PlayerCountImage2"));
        playerCountList.Add(GameObject.Find("PlayerCountImage3"));

        // ���A���AJump�{�^��
        operationButttons = GameObject.FindGameObjectsWithTag("OperationButtton");

        // Restart�{�^�����\��
        restartButton = GameObject.FindGameObjectWithTag("RestartButton");
        restartButton.SetActive(false);

        // �I�u�W�F�N�g�����ꂼ��Z�b�g����
        trains = GameObject.FindGameObjectWithTag("Train");
        mainBackground = GameObject.FindGameObjectWithTag("MainBackground");
        obstacleGenerator = GameObject.Find("ObstacleGenerator");

        // �S�w�����Z�b�g
        stationCountDown = STATION_SUM;

        // �J�E���g�_�E�����J�n
        gameText.text = $"{startCountDown}";

    }

    void Update()
    {
        // �Q�[���J�n�܂ŃJ�E���g�_�E��������
        StartCountDown();

        // �c�w�����e�L�X�g�\��
        stationCountDownText.text = $"�c��{stationCountDown}�w";
    }

    // �Q�[���J�n�܂ŃJ�E���g�_�E��������
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

                    // �d�Ԃ𓮂���
                    trains.GetComponent<TrainController>().StartedGame();

                    // �w�i�𓮂���
                    mainBackground.GetComponent<BackgroundController>().StartedGame();

                    // ��Q���𐶐�������
                    obstacleGenerator.GetComponent<ObstacleGenerator>().StartedGame();
                }
                else
                {
                    isStartedGame = true;
                    // �Q�[�����s���̓e�L�X�g��\��
                    gameText.gameObject.SetActive(false);

                }

            }

        }
    }

    public void CollideWithBlock()
    {
        // �v���C���[��UI���폜����(3��܂�)
        if (playerCount >= 0)
        {
            playerCountList[playerCount].SetActive(false);
            playerCount--;
        }
        // 4��ڂɏՓ˂����ꍇ
        else
        {
            FinishGame($"GAME OVER\nRecord�F{STATION_SUM - stationCountDown}/{STATION_SUM}�w�B���I");
        }

    }

    public void RestartButtonEnter()
    {
        SceneManager.LoadScene("TrainGameScene");
    }

    public void FinishGame(string finishedGameText)
    {
        // �Q�[���I�����Ƀe�L�X�g��\��
        gameText.text = finishedGameText;
        gameText.color = new Color(1, 1, 0, 1);
        gameText.gameObject.SetActive(true);

        // ���A���AJump�{�^�����\��
        foreach (var operationButtton in operationButttons)
        {
            operationButtton.SetActive(false);
        }

        // Restart�{�^����\��
        restartButton.SetActive(true);

        // ��Q�����o���Ȃ��悤�ɂ���
        GameObject obstacleGenerator = GameObject.Find("ObstacleGenerator");
        if (obstacleGenerator != null)
        {
            obstacleGenerator.GetComponent<ObstacleGenerator>().FinishGame();
        }

        // ��Q�������ɐ�������Ă����ꍇ�A�j������
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obstacle in obstacles)
        {
            obstacle.GetComponent<ObstacleController>().DestroyObstacle();
        }

        // �d�Ԃ��~�߂�
        trains.GetComponent<TrainController>().StopTrain();

        // �w�i�̓������~�߂�
        mainBackground.GetComponent<BackgroundController>().StopBackGround(true);
    }

    // �w���߂��Ȃ������Ƃ�ʒm����
    public void NotifyStation(string stationName)
    {
        string removeBlank = stationName.Replace("�@", "");
        gameText.text = $"�܂��Ȃ�{removeBlank}�w�ɓ������܂��B";
        gameText.gameObject.SetActive(true);

    }
    
    // ��ʐ^�񒆂̃e�L�X�g���\���ɂ���
    public void HiddenText()
    {
        gameText.gameObject.SetActive(false);
    }

    // �c�w�������炷
    public void StationCountDown()
    {
        stationCountDown--;
    }

    
}
