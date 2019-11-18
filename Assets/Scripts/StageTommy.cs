using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTommy : MonoBehaviour
{

    [SerializeField]
    private GameObject leftPlatform;

    [SerializeField]
    private GameObject rightPlatform;

    private float inputFactor = 2;
    private Vector3 currentRightPos;
    private Vector3 currentLeftPos;
    private int speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        currentRightPos = rightPlatform.transform.position;
        currentLeftPos = leftPlatform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // left platform
        leftPlatform.transform.Rotate(0, 0, 60 * Time.deltaTime);
        leftPlatform.transform.position = new Vector2(currentLeftPos.x + Mathf.Sin(Time.time * speed), currentLeftPos.y);

        // right platform
        rightPlatform.transform.position = new Vector2(currentRightPos.x, currentRightPos.y +  2 * Mathf.Sin(Time.time * speed));
    }
}
