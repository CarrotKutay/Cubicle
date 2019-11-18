using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DistanceWeapon
{


    private void WeaponFired()
    {
        if (CurrentAmmunition > 0)
        {
            //create Projectile Body PTBody
            GameObject PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PTBody.AddComponent<RocketLauncherProjectile>();
            PTBody.GetComponent<RocketLauncherProjectile>().addDamage(Damage);
            PTBody.transform.position = transform.position;// + Vector3.forward;
            PTBody.GetComponent<RocketLauncherProjectile>().BuildProjectile();

            //Fire Projectile
            Rigidbody rigidbody = PTBody.GetComponent<Rigidbody>();
            rigidbody.mass = 1.5f;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            rigidbody.AddForceAtPosition(FiringDirection * FiringStrength, PTBody.transform.position);

            // Remove Fired Projectile
            this.CurrentAmmunition = CurrentAmmunition - 1;
        }
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
