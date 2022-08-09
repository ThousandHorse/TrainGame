using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool LButtonDownFlag = false;
    private bool RButtonDownFlag = false;
    private const float PLAYER_SPEED = 0.2f;
    private float playerScale = 1.5f;
    private float limitX = 10.1f;
    private float limitY = 4.4f;

    uint jumpCount = 2;

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 400.0f;
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
        if (jumpCount != 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            this.animator.SetTrigger("JumpTrigger");
            jumpCount--;
        }
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        //isJumpping = false;
    }

    void Update()
    {
        moveKey = 0;

        if (LButtonDownFlag) moveKey = -1;
        if (RButtonDownFlag) moveKey = 1;

        // 左右移動
        transform.position += new Vector3(PLAYER_SPEED * moveKey, 0, 0);

        // 画面外にいかないようにする
        // 左右
        if (transform.position.x < -limitX)
        {
            transform.position = new Vector3(-limitX, transform.position.y, 0);
        }
        if (transform.position.x > limitX)
        {
            transform.position = new Vector3(limitX, transform.position.y, 0);
        }

        // 上
        if (transform.position.y > limitY)
        {
            transform.position = new Vector3(transform.position.x, limitY, 0);
        }

        // 水平向き反転
        if (moveKey != 0)
        {
            transform.localScale = new Vector3(playerScale * moveKey, playerScale, 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 電車に着地したとき
        if (collision.gameObject.CompareTag("TrainStage"))
        {
            jumpCount = 2;
        }
       
    }

}