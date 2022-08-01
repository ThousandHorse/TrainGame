using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    GameObject player;
    private float speed = -0.1f;
    float playerRadius = 0.15f;
    float blockRadius = 0.5f;
    
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(speed, 0, 0);

        /*
         * 当たり判定処理
         */

        //　中心座標
        Vector2 blockCenterCoordinates = transform.position;
        Vector2 playerCenterCoordinates = this.player.transform.position;　

        Vector2 dir = blockCenterCoordinates - playerCenterCoordinates;

        // PlayerとBlockの距離
        float d = dir.magnitude;

        // 衝突した場合
        if (playerRadius + blockRadius > d)
        {
            // プレイヤーの人数を減らす
            GameObject uiController = GameObject.Find("UIController");
            uiController.GetComponent<UIController>().CollideWithBlock();

            // ブロックのオブジェクトを破棄
            Destroy(gameObject);
        }

    }

    // 画面外に出たら破棄する
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
