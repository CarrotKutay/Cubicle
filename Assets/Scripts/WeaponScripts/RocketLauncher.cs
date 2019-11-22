using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DistanceWeapon
{

    protected override void addProjectile()
    {
        //create Projectile Body PTBody
        PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PTBody.AddComponent<RocketLauncherProjectile>();
        RocketLauncherProjectile projectile = PTBody.GetComponent<RocketLauncherProjectile>();
        projectile.addDamage(Damage);
        projectile.FiredFrom = gameObject.layer;
        PTBody.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        PTBody.transform.parent = null;
        projectile.BuildProjectile();
        PTBody.name = "RocketLauncherProjectile";
    }

    private void Start()
    {
        Init(10, 15, 10, 2);
        Reload();
        StartCoroutine(checkButtonFired());
    }
}
