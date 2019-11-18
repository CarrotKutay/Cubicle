using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float Speed, Velocity;
    protected LayerMask firedFrom;
    protected Rigidbody rb;
    protected BoxCollider ProjectileCollider;
    protected MeshRenderer rend;
    protected int Damage;
    public float ExplosionRadius { get; set; }
    public LayerMask FiredFrom { get => firedFrom; set => firedFrom = value; }

    public void addDamage(int value)
    {
        Damage = value;
    }
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
        gameObject.name = "Projectile";
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
        StartCoroutine(waitToClean());
    }
}
