using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : DistanceWeapon
{
    private Projectile projectile;
    protected override void addProjectile()
    {
        base.addProjectile();
        PTBody.AddComponent<Projectile>();
        projectile = PTBody.GetComponent<Projectile>();
        projectile.addDamage(Damage);
        projectile.FiredFrom = gameObject.layer;
        PTBody.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        PTBody.name = "Minigun-Projectile";
    }

    private void Start()
    {
        Init(100, 1, 30, 0.05f);
        reload();

    }
}
