using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    List<GameObject> playerCountList = new List<GameObject>();
    public TextMeshProUGUI uiText;

    void Start()
    {
        playerCountList.Add(GameObject.Find("PlayerCountImage1"));
        playerCountList.Add(GameObject.Find("PlayerCountImage2"));
        playerCountList.Add(GameObject.Find("PlayerCountImage3"));
        uiText.text = "START";
        uiText.color = new Color(1, 0, 0, 1);

    }

    public void CollideWithBlock()
    {
        // �v���C���[��UI���폜����(3��܂�)
        if (playerCountList.Count != 0)
        {
            Destroy(playerCountList[0]);
            playerCountList.RemoveAt(0);
        }
        // 4��ڂɏՓ˂����ꍇ
        else
        {
            uiText.text = "GAME OVER";
        }

    }
}
