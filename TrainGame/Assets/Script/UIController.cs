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
            FinishGame("GAME OVER");
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
        gameText.color = new Color(1, 0, 0, 1);
        gameText.gameObject.SetActive(true);

        // ���A���AJump�{�^�����\��
        foreach (var operationButtton in operationButttons)
        {
            operationButtton.SetActive(false);
        }

        // Restart�{�^����\��
        restartButton.SetActive(true);

        // ��Q�����o���Ȃ��悤�ɂ���
        GameObject blockGenerator = GameObject.Find("BlockGenerator");
        if (blockGenerator != null)
        {
            blockGenerator.GetComponent<BlockGenerator>().FinishGame();
        }

        // ��Q�������ɐ�������Ă����ꍇ�A�j������
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obstacle in obstacles)
        {
            obstacle.GetComponent<BlockController>().DestroyObstacle();
        }


        // �d�Ԃ��~�߂�
        for (int i = 0; i < SUM_TRAIN; i++)
        {
            string trainLocation = $"Train/Train{i}";
            trainArray[i] = GameObject.Find(trainLocation);
            trainArray[i].GetComponent<TrainController>().StopTrain();
        }

        // �w�i�̓������~�߂�
        GameObject mainBackground = GameObject.FindGameObjectWithTag("MainBackground");
        mainBackground.GetComponent<BackgroundController>().StopBackGround(true);
    }

    // �w���߂��Ȃ������Ƃ�ʒm����
    public void notifyStation(string stationName)
    {
        string removeBlank = stationName.Replace("�@","");
        gameText.text = $"�܂��Ȃ�{removeBlank}�w�ɓ������܂��B";
        gameText.gameObject.SetActive(true);

    }

    public void hiddenText()
    {
        gameText.gameObject.SetActive(false);
    }
}
