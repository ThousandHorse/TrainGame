using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject RBirdPrefab;
    public GameObject BBirdPrefab;
    public GameObject PBirdPrefab;
    GameObject[] birdPrefabs = new GameObject[3];
    float startPosX;
    float startPosY = -2.8f;
    float span;
    float delta = 0;

    int birdIndex;

    GameObject train;

    void Start()
    {
        birdPrefabs[0] = RBirdPrefab;
        birdPrefabs[1] = BBirdPrefab;
        birdPrefabs[2] = PBirdPrefab;

        this.span = Random.Range(1.5f, 4.0f);
        train = GameObject.FindGameObjectWithTag("Train");
        startPosX = train.transform.position.x;

    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > span)
        {
            this.delta = 0;
            startPosY = Random.Range(-2.8f, 1.0f);
            this.span = Random.Range(1.5f, 4.0f);

            // ’¹‚ðƒ‰ƒ“ƒ_ƒ€‚Å•\Ž¦‚³‚¹‚é
            birdIndex = Random.Range(0, birdPrefabs.Length - 1);

            //GameObject go = Instantiate(RBirdPrefab) as GameObject;
            GameObject go = Instantiate(birdPrefabs[birdIndex]) as GameObject;
            go.transform.position = new Vector3(startPosX, startPosY, 0);
        }
    }

    public void FinishGame()
    {
        Destroy(gameObject);
    }

    
}
