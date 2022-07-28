using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject blockPrefab; // ÝŒv}
    float span = 1.5f;
    float delta = 0;

    void Start()
    {
        
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(blockPrefab) as GameObject;
            go.transform.position = new Vector3(9.5f, -2.8f, 0);
        }
    }
}
