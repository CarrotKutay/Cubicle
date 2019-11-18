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

            // Remove Fired Projectile
            this.CurrentAmmunition = CurrentAmmunition - 1;
        }
    }

    private void addProjectile()
    {
        //create Projectile Body PTBody
        PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PTBody.AddComponent<RocketLauncherProjectile>();
        PTBody.GetComponent<RocketLauncherProjectile>().addDamage(Damage);
        PTBody.GetComponent<RocketLauncherProjectile>().FiredFrom = transform.parent.parent.gameObject.GetComponent<Character>().PersonalLayer;
        PTBody.transform.position = transform.parent.parent.position;
        PTBody.transform.parent = null;
        PTBody.GetComponent<RocketLauncherProjectile>().BuildProjectile();
    }

    private void fireProjectile()
    {
        //Fire Projectile
        Rigidbody rigidbody = PTBody.GetComponent<Rigidbody>();
        rigidbody.mass = 1.5f;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        rigidbody.AddForceAtPosition(FiringDirection * FiringStrength, PTBody.transform.position);
    }


    private IEnumerator checkButtonFired()
    {
        IsFiring = true;

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
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
        Init(10, 15, 500, 2);
        StartCoroutine(checkButtonFired());
    }
}
