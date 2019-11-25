using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    [SerializeField]
    private float gravityScale = 3.0f;

    private static float globalGravity = -9.81f;

    private Rigidbody rb;

    public float GravityScale { get => gravityScale; set => gravityScale = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * GravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}

