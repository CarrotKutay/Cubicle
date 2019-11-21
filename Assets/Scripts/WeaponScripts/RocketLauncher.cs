using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DistanceWeapon
{

    GameObject PTBody;

    private void WeaponFired()
    {
        if (CurrentAmmunition > 0)
        {
            addProjectile();
            fireProjectile();
            // Remove Fired Projectile
            this.CurrentAmmunition = CurrentAmmunition - 1;
        }
    }

    private void addProjectile()
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

    private void fireProjectile()
    {
        //Fire Projectile
        Rigidbody rigidbody = PTBody.GetComponent<Rigidbody>();
        rigidbody.mass = 1.5f;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        rigidbody.AddForceAtPosition(FiringDirection.normalized * 10, PTBody.transform.position, ForceMode.Impulse);
    }


    private IEnumerator checkButtonFired()
    {
        IsFiring = true;

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetAxis("RightTrigger") > 0)
        {
            WeaponFired();
            yield return new WaitForSeconds(FiringRate);
        }

        IsFiring = false;
    }

    protected override void shoot()
    {
        if (!IsFiring)
        {
            StartCoroutine(checkButtonFired());
        }
    }

    private void Start()
    {
        Init(10, 15, 1, 2);
        Reload();
        StartCoroutine(checkButtonFired());
    }
}
