using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject blockPrefab; // ÝŒv}
    float delta = 0;
    float startPosX = 14.0f;
    float startPosY = -2.8f;
    float span;

    void Start()
    {
        this.span = Random.Range(1.0f, 4.0f);
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > span)
        {
            this.delta = 0;
            this.span = Random.Range(1.0f, 4.0f);
            GameObject go = Instantiate(blockPrefab) as GameObject;
            go.transform.position = new Vector3(startPosX, startPosY, 0);
        }
    }

    public void FinishGame()
    {
        Destroy(gameObject);
    }

    
}
