using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTommy : MonoBehaviour
{

    [SerializeField]
    private GameObject leftPlatform;
    [SerializeField]
    private GameObject rightPlatform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftPlatform.transform.Rotate(0, 0, 60 * Time.deltaTime);

        rightPlatform.transform.position = new Vector3(rightPlatform.transform.position.x, 2 * Time.time, rightPlatform.transform.position.z);

        if (rightPlatform.transform.position.y > 3.0f)
        {
            
        }
    }
}
