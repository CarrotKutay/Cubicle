using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float Speed, Velocity;
    protected Rigidbody rb;
    protected BoxCollider ProjectileCollider;
    protected MeshRenderer rend;
    protected float TimeofCreation;

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

    /**
        ** cleaning up projectile */
    protected void cleanup()
    {
        Destroy(this.gameObject);
    }
    protected IEnumerator waitToClean()
    {
        yield return new WaitForSeconds(7);
        cleanup();
    }
    private void Start()
    {
        TimeofCreation = Time.time;
        StartCoroutine(waitToClean());
    }
}
