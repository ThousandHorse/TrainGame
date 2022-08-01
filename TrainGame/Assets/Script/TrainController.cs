using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainController : MonoBehaviour
{
    private int frameCounter = 0;
    private float trainTransform = 0.005f;
    float span = 0.1f;
    float delta = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, trainTransform, 0);
        frameCounter++;

        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            frameCounter = 0;
            trainTransform *= -1;

        }
    }
}
