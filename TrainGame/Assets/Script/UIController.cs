using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    const int SUM_TRAIN = 9;
    const int SUM_BACKGROUND = 6;

    GameObject[] backgroundArray = new GameObject[SUM_BACKGROUND];
    GameObject[] trainArray = new GameObject[SUM_TRAIN];
    List<GameObject> playerCountList = new List<GameObject>();

    public TextMeshProUGUI finishedGameText;
    public TextMeshProUGUI stationName;
    public GameObject operationButtton;
    public GameObject restartButton;
    int stationCount = 0;
    int playerCount = 2;

    string startStation = "�ܔ��c";
    string[] stationArray = new string[29]
        {
            "��@��","�i�@��","�c�@��","�l����","�V�@��","�L�y��",
            "���@��","�_�@�c","�H�t��","��k��","��@��","��@�J",
            "���闢","�����闢","�c�@�[","��@��","���@��","��@��",
            "�r�@��","�ځ@��","���c�n��","�V��v��","�V�@�h","��X��",
            "���@�h","�a�@�J","�b���","�ځ@��","�ܔ��c"
        };

    void Start()
    {
        // �v���C���[�̎c�l�����Z�b�g
        playerCountList.Add(GameObject.Find("PlayerCountImage1"));
        playerCountList.Add(GameObject.Find("PlayerCountImage2"));
        playerCountList.Add(GameObject.Find("PlayerCountImage3"));

        // ���A���AJump�{�^��
        operationButtton = GameObject.Find("Canvas/Button");

        // Restart�{�^�����\��
        restartButton = GameObject.Find("Canvas/RestartButton");
        restartButton.SetActive(false);

        // �w��
        stationName.color = new Color(0, 0, 0, 1);
        stationName.text = startStation;

        // �Q�[�����s���͔�\��
        finishedGameText.gameObject.SetActive(false);


    }
    void Update()
    {
       
    }

    public void changeStation()
    {
        if (stationCount < stationArray.Length)
        {
            stationName.text = stationArray[stationCount];
            stationCount++;
        }
        else
        {
            FinishGame("GAME CLEAR");
            stationCount = 0;
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
            FinishGame("GAME OVER");
        }

    }

    public void RestartButtonEnter()
    {
        SceneManager.LoadScene("TrainGameScene");
    }

    private void FinishGame(string uiText)
    {
        // �Q�[���I�����Ƀe�L�X�g��\��
        finishedGameText.text = uiText;
        finishedGameText.color = new Color(1, 0, 0, 1);
        finishedGameText.gameObject.SetActive(true);

        // ���A���AJump�{�^�����\��
        operationButtton.SetActive(false);

        // Restart�{�^����\��
        restartButton.SetActive(true);

        // ��Q�����o���Ȃ��悤�ɂ���
        GameObject blockGenerator = GameObject.Find("BlockGenerator");
        if (blockGenerator != null)
        {
            blockGenerator.GetComponent<BlockGenerator>().FinishGame();
        }

        // �d�Ԃ��~�߂�
        for (int i = 0; i < SUM_TRAIN; i++)
        {
            string trainLocation = $"Train/Train{i}";
            trainArray[i] = GameObject.Find(trainLocation);
            trainArray[i].GetComponent<TrainController>().StopTrain();
        }

        // �w�i�̓������~�߂�
        for (int i = 0; i < SUM_BACKGROUND; i++)
        {
            string bgLocation = $"Background/Background{i}";
            GameObject backgroundController = GameObject.Find(bgLocation);
            backgroundController.GetComponent<BackgroundController>().StopBackGround();
        } 
    }
}
