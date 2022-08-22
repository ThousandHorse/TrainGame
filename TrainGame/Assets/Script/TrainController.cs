using UnityEngine;

public class TrainController : MonoBehaviour
{
    private float trainTransform = 0.003f;
    float span = 0.1f;
    float delta = 0;
    bool isStopped = false;
    bool isStartedGame = false;

    void Update()
    {
        if (isStartedGame && !isStopped)
        {
            transform.position += new Vector3(0, trainTransform, 0);
            this.delta += Time.deltaTime;

            if (this.delta > this.span)
            {
                this.delta = 0;
                trainTransform *= -1;

            }

            //GetComponent<AudioSource>().Play();
        }
    }

    public void StopTrain()
    {
        isStopped = true;
    }

    public void RunTrain()
    {
        isStopped = false;
    }

    public void StartedGame()
    {
        isStartedGame = true;
    }

}
