using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherProjectile : Projectile
{
    public void BuildProjectile()
    {
        Init();

        //scaling
        gameObject.transform.localScale = new Vector3(0.3f, 0.2f, 0.2f);
        gameObject.AddComponent<Explosion>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Collision");
            GetComponent<Explosion>().Explode(collision.collider.transform.position);
            //Destroy(this);
        }
    }
    
}
