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
        // �v���C���[�̎c�l�����Z�b�g
        playerCountList.Add(GameObject.Find("PlayerCountImage1"));
        playerCountList.Add(GameObject.Find("PlayerCountImage2"));
        playerCountList.Add(GameObject.Find("PlayerCountImage3"));

        // ���A���AJump�{�^��
        operationButttons = GameObject.FindGameObjectsWithTag("OperationButtton");

        // Restart�{�^�����\��
        restartButton = GameObject.FindGameObjectWithTag("RestartButton");
        restartButton.SetActive(false);

        // �Q�[�����s���͔�\��
        gameText.gameObject.SetActive(false);

        trains = GameObject.FindGameObjectWithTag("Train");
        mainBackground = GameObject.FindGameObjectWithTag("MainBackground");


        stationCountDown = STATION_SUM_TEST + 1;

    }

    void Update()
    {
        // �c�w�����e�L�X�g�\��
        stationCountDownText.text = $"�c��{stationCountDown}�w";
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
            FinishGame($"GAME OVER\n�L�^�F{STATION_SUM_TEST - stationCountDown}/{STATION_SUM_TEST}�w�B���I");
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

    public void HiddenText()
    {
        gameText.gameObject.SetActive(false);
    }

    public void StationCountDown()
    {
        // �c�w�������炷
        stationCountDown--;
    }
}
