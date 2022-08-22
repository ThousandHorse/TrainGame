using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool LButtonDownFlag = false;
    private bool RButtonDownFlag = false;
    private const float PLAYER_SPEED = 0.2f;
    private float playerScale = 1.5f;
    private float limitX = 10.1f;
    private float limitY = 4.4f;
    
    // �_�ŗp
    private Renderer playerRend;
    // �_�Ŏ���
    const float FLASHING_SPAN = 0.05f;

    // ���G����
    const float INVINCIBLE_SPAN = 2.0f;

    // �_�ŊԊu
    float currentTime = 0;

    // �_�ł��鎞��(���G����)
    float invincibleTime = 0;

    uint jumpCount = 2;

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 400.0f;
    int moveKey;

    bool isCollision;

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
        playerRend = GetComponent<Renderer>();
    }

    void Update()
    {
        moveKey = 0;

        if (LButtonDownFlag) moveKey = -1;
        if (RButtonDownFlag) moveKey = 1;

        // ���E�ړ�
        transform.position += new Vector3(PLAYER_SPEED * moveKey, 0, 0);

        // ��ʊO�ɂ����Ȃ��悤�ɂ���
        // ���E
        if (transform.position.x < -limitX)
        {
            transform.position = new Vector3(-limitX, transform.position.y, 0);
        }
        if (transform.position.x > limitX)
        {
            transform.position = new Vector3(limitX, transform.position.y, 0);
        }

        // ��
        if (transform.position.y > limitY)
        {
            transform.position = new Vector3(transform.position.x, limitY, 0);
        }

        // �����������]
        if (moveKey != 0)
        {
            transform.localScale = new Vector3(playerScale * moveKey, playerScale, 1);
        }


        if (isCollision)
        {
            currentTime += Time.deltaTime;
            invincibleTime += Time.deltaTime;

            // �_�ł�����
            if (currentTime > FLASHING_SPAN)
            {
                playerRend.enabled = !playerRend.enabled;
                currentTime = 0;

                // TODO; �����蔻���OFF�ɂ���
            }
            // ���G���Ԍo�ߌ�A�_�ł����Ȃ��悤�ɂ���
            if (invincibleTime > INVINCIBLE_SPAN)
            {
                isCollision = false;
                invincibleTime = 0;
                playerRend.enabled = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �d�Ԃɒ��n�����Ƃ�
        if (collision.gameObject.CompareTag("TrainStage"))
        {
            jumpCount = 2;
        }

        // ��Q���ɏՓ˂����Ƃ�
        if (!isCollision && collision.gameObject.CompareTag("Obstacle"))
        {
            // �G�t�F�N�g��r�o����
            //gameObject.GetComponent<ParticleSystem>().Play();

            // �_�ł�����悤�ɂ���
            isCollision = true;
        }

    }

}