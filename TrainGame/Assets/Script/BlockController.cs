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
        // ���x����
        transform.Translate(speed, 0, 0);

        // �傫��
        transform.localScale = new Vector3(scaleX, scaleY, 0);

    }

    // ��ʊO�ɏo����j������
    private void OnBecameInvisible()
    {
        DestroyObstacle();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �v���C���[�ƏՓ˂����ꍇ
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�̐l�������炷
            GameObject uiController = GameObject.Find("UIController");
            uiController.GetComponent<UIController>().CollideWithBlock();

            // �u���b�N�̃I�u�W�F�N�g��j��
            DestroyObstacle();
        }
    }

    public void DestroyObstacle()
    {
        Destroy(gameObject);
    }
}
