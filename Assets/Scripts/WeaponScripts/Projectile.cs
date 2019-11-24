using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected LayerMask firedFrom;
    protected Rigidbody rb;
    protected MeshRenderer rend;
    protected int Damage;
    public float ExplosionRadius { get; set; }
    public LayerMask FiredFrom { get => firedFrom; set => firedFrom = value; }

    public void addDamage(int value)
    {
        Damage = value;
    }
    private void Init()
    {
        //create projectile
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();

        //reference components
        rb = GetComponent<Rigidbody>();
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

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        StartCoroutine(waitToClean());
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != FiredFrom)
        {

            if (other.gameObject.TryGetComponent<Character>(out Character player))
            {
                player.UpdateHealth(Damage);
            }
        }
    }
}
