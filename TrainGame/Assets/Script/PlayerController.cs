using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool LButtonDownFlag = false;
    private bool RButtonDownFlag = false;
    private float moveSpeed = 0.2f;
    private float playerScale = 0.3f;
    private float limitX = 10.1f;
    private bool isJumpping;

    Rigidbody2D rigid2D;
    float jumpForce = 370.0f;
    int moveKey;

    public void LButtonDown()
    {
        LButtonDownFlag = true;
    }

    public void RButtonDown()
    {
        RButtonDownFlag = true;
    }

    public void LButtonUp()
    {
        LButtonDownFlag = false;
    }

    public void RButtonUp()
    {
        RButtonDownFlag = false;
    }

    public void JumpButton()
    {
        // ジャンプを着地中のみ行うよう制御する
        if (!isJumpping)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            isJumpping = true;
        }
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        isJumpping = false;
    }

    void Update()
    {
        moveKey = 0;

        if (LButtonDownFlag) moveKey = -1;
        if (RButtonDownFlag) moveKey = 1;

        transform.position += new Vector3(moveSpeed * moveKey, 0, 0);

        // 左右移動
        if (transform.position.x < -limitX)
        {
            transform.position = new Vector3(-limitX, transform.position.y, 0);
        }
        if (transform.position.x > limitX)
        {
            transform.position = new Vector3(limitX, transform.position.y, 0);
        }

        // 水平向き反転
        if (moveKey != 0)
        {
            transform.localScale = new Vector3(playerScale * moveKey, playerScale, 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 電車(ステージ)に着地したとき
        if (collision.gameObject.CompareTag("Stage"))
        {
            isJumpping = false;
        }
       
    }
}