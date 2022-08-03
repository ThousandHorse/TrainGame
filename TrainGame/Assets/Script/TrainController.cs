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
    bool isStopped = false;

    void Update()
    {
        if (!isStopped)
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

    public void StopTrain()
    {
        isStopped = true;
    }

}
