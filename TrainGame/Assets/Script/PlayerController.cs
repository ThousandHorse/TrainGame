using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool LButtonDownFlag = false;
    private bool RButtonDownFlag = false;
    private float movingSpeed = 0.01f;

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;

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
        this.rigid2D.AddForce(transform.up * this.jumpForce);
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (LButtonDownFlag)
        {
            transform.position -= new Vector3(movingSpeed, 0, 0);
        }

        if (RButtonDownFlag)
        {
            transform.position += new Vector3(movingSpeed, 0, 0);
        }

    }
}
