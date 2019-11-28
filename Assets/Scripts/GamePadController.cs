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

    private int controllerNumber;

    private string throwInput;
    private string leftJoystickHor;
    private string aButton;
    private Character character;

    public int ControllerNumber { get => controllerNumber; set => controllerNumber = value; }

    void Start()
    {
        character = GetComponent<Character>();

        rb = GetComponent<Rigidbody>();
        startPos.x = gameObject.transform.position.x;
        startPos.y = gameObject.transform.position.y;
        setControllerToPlayer();
    }

    void setControllerToPlayer()
    {
        if (ControllerNumber == 1)
        {
            aButton = "AButton" + ControllerNumber;
            leftJoystickHor = "LeftJoystickHorizontal" + ControllerNumber;
            throwInput = "ThrowGP" + ControllerNumber;
            aButton = "AButton" + ControllerNumber;
            leftJoystickHor = "LeftJoystickHorizontal" + ControllerNumber;
        }
        else if (ControllerNumber == 2)
        {
            aButton = "AButton" + ControllerNumber;
            leftJoystickHor = "LeftJoystickHorizontal" + ControllerNumber;
            throwInput = "ThrowGP" + ControllerNumber;

            aButton = "AButton" + ControllerNumber;
            leftJoystickHor = "LeftJoystickHorizontal" + ControllerNumber;
        }
    }

    void FixedUpdate()
    {
        // Bewegung nach links und rechts mit den Pfeiltasten. 
        moveInput = Input.GetAxis(leftJoystickHor);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Sprung und Doppelsprung mit der Pfeiltaste nach oben.
        if (Input.GetButtonDown(aButton) && extraJumps == 2)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        else if (Input.GetButtonDown(aButton) && extraJumps == 1)
        {
            rb.velocity = Vector2.up * jumpForce * 0.8f;
            extraJumps--;
        }

        else if (Input.GetButtonDown(aButton) && extraJumps == 0)
        {
            rb.velocity = Vector2.up * 0;
        }

        //throw weapon
        if (Input.GetAxis(throwInput) > 0)
        {
            character.throwWeapon();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            extraJumps = 2;
        }
    }
}
