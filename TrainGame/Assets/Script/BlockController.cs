using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    GameObject player;
    private float speed = -0.1f;
    float playerRadius = 0.15f;
    float blockRadius = 0.5f;
    
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(speed, 0, 0);

        /*
         * �����蔻�菈��
         */

        //�@���S���W
        Vector2 blockCenterCoordinates = transform.position;
        Vector2 playerCenterCoordinates = this.player.transform.position;�@

        Vector2 dir = blockCenterCoordinates - playerCenterCoordinates;

        // Player��Block�̋���
        float d = dir.magnitude;

        // �Փ˂����ꍇ
        if (playerRadius + blockRadius > d)
        {
            // �v���C���[�̐l�������炷
            GameObject uiController = GameObject.Find("UIController");
            uiController.GetComponent<UIController>().CollideWithBlock();

            // �u���b�N�̃I�u�W�F�N�g��j��
            Destroy(gameObject);
        }

    }

    // ��ʊO�ɏo����j������
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
