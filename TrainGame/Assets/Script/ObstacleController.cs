using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float speed = -0.07f;
    
    void Start()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        // 速度調整
        transform.position += new Vector3(speed, 0, 0);
        
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
