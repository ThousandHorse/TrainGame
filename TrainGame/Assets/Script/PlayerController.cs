using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool LButtonDownFlag = false;
    private bool RButtonDownFlag = false;
    private float moveSpeed = 0.2f;
    private float playerScale = 1.5f;
    private float limitX = 10.1f;
    private bool isJumpping;

    Rigidbody2D rigid2D;
    Animator animator;
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
        // �W�����v�𒅒n���̂ݍs���悤���䂷��
        if (!isJumpping)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            this.animator.SetTrigger("JumpTrigger");
            isJumpping = true;
        }
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isJumpping = false;
    }

    void Update()
    {
        moveKey = 0;

        if (LButtonDownFlag) moveKey = -1;
        if (RButtonDownFlag) moveKey = 1;

        // ���E�ړ�
        transform.position += new Vector3(moveSpeed * moveKey, 0, 0);

        // ��ʊO�ɂ����Ȃ��悤�ɂ���
        if (transform.position.x < -limitX)
        {
            transform.position = new Vector3(-limitX, transform.position.y, 0);
        }
        if (transform.position.x > limitX)
        {
            transform.position = new Vector3(limitX, transform.position.y, 0);
        }

        // �����������]
        if (moveKey != 0)
        {
            transform.localScale = new Vector3(playerScale * moveKey, playerScale, 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �d�Ԃɒ��n�����Ƃ�
        if (collision.gameObject.CompareTag("Train"))
        {
            isJumpping = false;
        }
       
    }

}