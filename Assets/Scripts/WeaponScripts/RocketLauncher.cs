using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DistanceWeapon
{
    private RocketLauncherProjectile projectile;
    protected override void addProjectile()
    {
        base.addProjectile();
        PTBody.AddComponent<RocketLauncherProjectile>();
        projectile = PTBody.GetComponent<RocketLauncherProjectile>();
        projectile.addDamage(Damage);
        projectile.FiredFrom = gameObject.layer;
        projectile.BuildProjectile();
        PTBody.name = "RocketLauncherProjectile";
    }

    private void Start()
    {
        Init(10, 15, 10, 2, 8f);
        reload();
        StartCoroutine(checkButtonFired());
    }
}
