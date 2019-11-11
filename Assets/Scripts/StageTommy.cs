using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTommy : MonoBehaviour
{

    [SerializeField]
    private GameObject leftPlatform;

    [SerializeField]
    private GameObject rightPlatform;
    private Vector3 startPos;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = rightPlatform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        leftPlatform.transform.Rotate(0, 0, 60 * Time.deltaTime);

        newPos = startPos;
        newPos.x = newPos.x + Mathf.PingPong(Time.deltaTime, 6) - 3;

        rightPlatform.transform.position = newPos;
        // rightPlatform.transform.position.x = newPos.x;
    }
}
