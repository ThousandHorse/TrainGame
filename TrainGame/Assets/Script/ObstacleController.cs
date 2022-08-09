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
        // ���x����
        transform.position += new Vector3(speed, 0, 0);
        
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
