using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text.color = new Color(0, 1, 0, 1);
    }

    // StartScene
    public void startButton()
    {
        SceneManager.LoadScene("TrainGameScene");
    }
}
