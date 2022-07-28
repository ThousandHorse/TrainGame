using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    GameObject player;
    private float speed = -0.01f;
    private float screenMinPosition = -9.5f;
    float playerRadius = 0.15f;
    float blockRadius = 0.5f;
    
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(speed, 0, 0);

        // ��ʊO�ɏo����j������
        if (transform.position.x < screenMinPosition)
        {
            Destroy(gameObject);
        }

        //�@�����蔻��

        //�@���S���W
        Vector2 blockCenterCoordinates = transform.position;
        Vector2 playerCenterCoordinates = this.player.transform.position;�@

        Vector2 dir = blockCenterCoordinates - playerCenterCoordinates;

        // Player��Block�̋���
        float d = dir.magnitude;

        // �Փ˂����ꍇ�̓I�u�W�F�N�g��j��
        if (playerRadius + blockRadius > d)
        {
            GameObject uiController = GameObject.Find("UIController");
            // �v���C���[�̐l�������炷
            uiController.GetComponent<UIController>().CollideWithBlock();
            Destroy(gameObject);
        }

    }
}
