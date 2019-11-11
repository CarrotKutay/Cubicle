using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherProjectile : Projectile
{
    public Vector3 ScaleSize;

    public void BuildProjectile()
    {
        Init();

        //* scaling projectile
        ScaleSize = new Vector3(0.2f, 0.1f, 0.1f);
        gameObject.transform.localScale = ScaleSize;

        //* adding explosive behaviour to rocketlauncher projectiles */
        gameObject.AddComponent<Explosion>();
    }
    /**
        **  Checking for Collision with a target
        **  After Target was hit projectile will be cleaned up to not clutter scene
        */
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Explosion>().Explode(collision.GetContact(0).point);
        cleanup();
    }

}
