using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    List<GameObject> playerCountList = new List<GameObject>();
    GameObject[] trainArray = new GameObject[9];
    public TextMeshProUGUI finishedGameText;
    public TextMeshProUGUI stationName;
    public GameObject operationButtton;
    public GameObject restartButton;
    //float delta = 0;
    //float span = 1.0f;
    int stationCount = 0;
    int playerCount = 2;
    //bool isFinishedGame = false;

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
        //if (!isFinishedGame)
        //{
        //    this.delta += Time.deltaTime;
        //    if (this.delta > this.span)
        //    {
        //        this.delta = 0;
        //        stationCount++;
        //        if (stationCount < stationArray.Length)
        //        {
        //            stationName.text = stationArray[stationCount];
        //        }
        //        else
        //        {
        //            FinishGame("GAME CLEAR");
        //        }
        //    }
        //}

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
        trainArray[0] = GameObject.Find("Train/Train0");
        trainArray[1] = GameObject.Find("Train/Train1");
        trainArray[2] = GameObject.Find("Train/Train2");
        trainArray[3] = GameObject.Find("Train/Train3");
        trainArray[4] = GameObject.Find("Train/Train4");
        trainArray[5] = GameObject.Find("Train/Train5");
        trainArray[6] = GameObject.Find("Train/Train6");
        trainArray[7] = GameObject.Find("Train/Train7");
        trainArray[8] = GameObject.Find("Train/Train8");

        foreach (var train in trainArray)
        {
            train.GetComponent<TrainController>().StopTrain();
        }

        // �w�i�̓������~�߂�
        GameObject backgroundController = GameObject.Find("BackgroundController");
        backgroundController.GetComponent<BackgroundController>().StopBackGround();

        //isFinishedGame = true;

    }
}
