using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    GameObject player;
    private float speed = -0.1f;
    private float scaleX;
    private float scaleY;
    
    void Start()
    {
        this.player = GameObject.Find("Player");
        scaleX = Random.Range(1.0f, 2.7f);
        scaleY = Random.Range(0.7f, 1.5f);
    }

    void Update()
    {
        // 速度調整
        transform.Translate(speed, 0, 0);

        // 大きさ
        transform.localScale = new Vector3(scaleX, scaleY, 0);

    }

    // 画面外に出たら破棄する
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーと衝突した場合
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーの人数を減らす
            GameObject uiController = GameObject.Find("UIController");
            uiController.GetComponent<UIController>().CollideWithBlock();

            // ブロックのオブジェクトを破棄
            Destroy(gameObject);
        }
    }
}
