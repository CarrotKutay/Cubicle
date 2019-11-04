using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DistanceWeapon
{
    public void Fire()
    {

    }

    private void Start()
    {
        GameObject PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PTBody.AddComponent<RocketLauncherProjectile>();
        PTBody.transform.position = transform.position;
        PTBody.GetComponent<RocketLauncherProjectile>().BuildProjectile();
    }
}
