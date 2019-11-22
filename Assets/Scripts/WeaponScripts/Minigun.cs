using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : DistanceWeapon
{
    protected override void addProjectile()
    {
        //create Projectile Body PTBody
        PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PTBody.AddComponent<Projectile>();
        Projectile projectile = PTBody.GetComponent<Projectile>();
        projectile.addDamage(Damage);
        projectile.FiredFrom = gameObject.layer;
        PTBody.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        PTBody.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        PTBody.transform.parent = null;
        PTBody.name = "Minigun-Projectile";
    }

    private void Start()
    {
        Init(100, 1, 30, 0.05f);
        Reload();
    }
}
