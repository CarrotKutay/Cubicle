using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startPos;

    [SerializeField]
    float speed = 7;
    [SerializeField]
    float jumpForce = 13;

    private float moveInput;

    private int extraJumps = 2;

    // private int playerLifes = 3;

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
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Sprung und Doppelsprung mit der Pfeiltaste nach oben.
        if (Input.GetButtonDown("Jump") && extraJumps == 2)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        else if (Input.GetButtonDown("Jump") && extraJumps == 1)
        {
            rb.velocity = Vector2.up * jumpForce * 0.8f;
            extraJumps--;
        }

        else if (Input.GetButtonDown("Jump") && extraJumps == 0)
        {
            rb.velocity = Vector2.up * 0;
        }

        //throw weapon
        if (Input.GetButtonDown("Throw"))
        {
            GetComponent<Character>().throwWeapon();
        }

        // Respawn bei mehreren Leben
        /*
        if (gameObject.transform.position.y < -5.0f)
        {
            Invoke("respawn", 2.0f);
        }
        */
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
