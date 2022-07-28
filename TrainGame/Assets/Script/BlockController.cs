using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    GameObject player;
    private float speed = -0.01f;
    private float screenMinPosition = -9.5f;
    float playerRadius = 0.15f;
    float blockRadius = 0.5f;
    
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(speed, 0, 0);

        // 画面外に出たら破棄する
        if (transform.position.x < screenMinPosition)
        {
            Destroy(gameObject);
        }

        //　当たり判定

        //　中心座標
        Vector2 blockCenterCoordinates = transform.position;
        Vector2 playerCenterCoordinates = this.player.transform.position;　

        Vector2 dir = blockCenterCoordinates - playerCenterCoordinates;

        // PlayerとBlockの距離
        float d = dir.magnitude;

        // 衝突した場合はオブジェクトを破棄
        if (playerRadius + blockRadius > d)
        {
            GameObject uiController = GameObject.Find("UIController");
            // プレイヤーの人数を減らす
            uiController.GetComponent<UIController>().CollideWithBlock();
            Destroy(gameObject);
        }

    }
}
