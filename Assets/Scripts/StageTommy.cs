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

    private float rightXStart;
    private float rightXChange;


    private float rightYStart;
    private float rightYChange;

    // Start is called before the first frame update
    void Start()
    {
        // current x position of the right platform
        rightXStart = rightPlatform.transform.position.x;
        // the change of that x value
        rightXChange = Random.Range(1.0f, 2.0f) * 0.5f * Time.deltaTime;

        // current y position of the right platform
        rightYStart = rightPlatform.transform.position.y;
        // the change of that y value
        rightYChange = Random.Range(1.0f, 2.0f) * 0.5f * Time.deltaTime;

        // current position of the left platform
        currentLeftPos = leftPlatform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // left platform
        leftPlatform.transform.Rotate(0, 0, 60 * Time.deltaTime);
        leftPlatform.transform.position = new Vector2(currentLeftPos.x + Mathf.Sin(Time.time * speed), currentLeftPos.y);

        // calculate new x and y position of the right platform
        float rightXnew = rightPlatform.transform.position.x + rightXChange;
        float rightYnew = rightPlatform.transform.position.y + rightYChange;

        rightPlatform.transform.position = new Vector3(rightXnew, rightYnew, 0);

        if ((rightYnew > rightYStart + 2) || (rightYnew < rightYStart - 2))
        {
            rightYChange = -1 * rightYChange;
        }

        if ((rightXnew > rightXStart + 2) || (rightXnew < rightXStart - 2))
        {
            rightXChange = -1 * rightXChange;
        }
    }
}
