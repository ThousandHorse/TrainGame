using UnityEngine;

public class BlockController : MonoBehaviour
{
    private float speed = -0.1f;
    private float scaleX;
    private float scaleY;
    
    void Start()
    {
        scaleX = Random.Range(1.0f, 2.0f);
        scaleY = Random.Range(0.7f, 1.0f);
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
        DestroyObstacle();
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
            DestroyObstacle();
        }
    }

    public void DestroyObstacle()
    {
        Destroy(gameObject);
    }
}
