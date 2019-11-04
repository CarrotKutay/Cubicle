using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float Speed, Velocity;
    protected Rigidbody rb;
    protected BoxCollider ProjectileCollider;
    protected MeshRenderer rend;

    protected void Init()
    {
        
        //create projectile
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();
        
        //reference components
        rb = GetComponent<Rigidbody>();
        ProjectileCollider = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
        rend.material = Resources.Load<Material>("ProjectileMaterial");
    }
}
