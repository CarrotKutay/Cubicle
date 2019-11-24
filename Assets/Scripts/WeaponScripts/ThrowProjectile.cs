using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : Projectile
{
    private Vector3 throwingDirection;
    public Vector3 ThrowingDirection { get => throwingDirection; set => throwingDirection = value; }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != this.FiredFrom)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Character player = other.gameObject.GetComponent<Character>();
                player.UpdateHealth(-Damage);
            }
        }
    }

    public void startThrow()
    {
        Debug.Log(throwingDirection.ToString() + ", " + transform.position.ToString());
        GetComponent<Rigidbody>().AddForceAtPosition(throwingDirection * 10, transform.position);
    }
}
