using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    GameObject background1;
    GameObject background2;
    GameObject background3;
    GameObject background4;
    Vector3 defaultPos1;
    Vector3 defaultPos2;
    Vector3 defaultPos3;
    Vector3 defaultPos4;
    float speed = -0.05f;

    void Start()
    {
        background1 = GameObject.Find("Background1");
        background2 = GameObject.Find("Background2");
        background3 = GameObject.Find("Background3");
        background4 = GameObject.Find("Background4");

        defaultPos1 = background1.transform.position;
        defaultPos2 = background2.transform.position;
        defaultPos3 = background3.transform.position;
        defaultPos4 = background4.transform.position;
    }

    void Update()
    {
        background1.transform.position += new Vector3(speed, 0, 0);
        background2.transform.position += new Vector3(speed, 0, 0);
        background3.transform.position += new Vector3(speed, 0, 0);
        background4.transform.position += new Vector3(speed, 0, 0);

        if (background4.transform.position.x <= defaultPos1.x)
        {
            //@”wŒi‚ðÅ‰‚ÌˆÊ’u‚É–ß‚·(ƒ‹[ƒv)
            background1.transform.position = defaultPos1;
            background2.transform.position = defaultPos2;
            background3.transform.position = defaultPos3;
            background4.transform.position = defaultPos4;

            //@‰w–¼‚ð•Ï‚¦‚é
            GameObject uiController = GameObject.Find("UIController");
            uiController.GetComponent<UIController>().changeStation();

        }
    }
    public void StopBackGround()
    {
        speed = 0;
    }
}
