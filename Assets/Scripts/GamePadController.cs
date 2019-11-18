using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadController : MonoBehaviour
{
    private Vector2 startPos;

    [SerializeField]
    float speed = 7;
    [SerializeField]
    float jumpForce = 13;

    private float moveInput;

    private int extraJumps = 2;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos.x = gameObject.transform.position.x;
        startPos.y = gameObject.transform.position.y; 
    }

    void FixedUpdate()
    {
        // Bewegung nach links und rechts mit den Pfeiltasten. 
        moveInput = Input.GetAxis("LeftJoystickHorizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Sprung und Doppelsprung mit der Pfeiltaste nach oben.
        if (Input.GetButtonDown("AButton") && extraJumps == 2)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        else if (Input.GetButtonDown("AButton") && extraJumps == 1)
        {
            rb.velocity = Vector2.up * jumpForce * 0.8f;
            extraJumps--;
        }

        else if (Input.GetButtonDown("AButton") && extraJumps == 0)
        {
            rb.velocity = Vector2.up * 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            extraJumps = 2;
        }
    }

    private void respawn()
    {
        gameObject.transform.position = startPos;
    }
}
