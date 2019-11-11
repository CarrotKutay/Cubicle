using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DistanceWeapon
{

    private bool isFiring = false;

    private void WeaponFired()
    {
        if (CurrentAmmunition > 0)
        {
            //Get Firing Direction
            getCursorPosition();
            //create Projectile Body PTBody
            GameObject PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PTBody.AddComponent<RocketLauncherProjectile>();
            PTBody.GetComponent<RocketLauncherProjectile>().addDamage(Damage);
            PTBody.transform.position = transform.position;
            PTBody.GetComponent<RocketLauncherProjectile>().BuildProjectile();
            PTBody.transform.Rotate(0, 0, firingDirection.x / firingDirection.y);

            //Fire Projectile
            Rigidbody rigidbody = PTBody.GetComponent<Rigidbody>();
            rigidbody.mass = 1.5f;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            rigidbody.AddForceAtPosition(firingDirection * FiringStrength, PTBody.transform.position);

            // Remove Fired Projectile
            this.CurrentAmmunition = CurrentAmmunition - 1;
        }
    }


    private IEnumerator checkButtonFired()
    {
        isFiring = true;

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            WeaponFired();
            yield return new WaitForSeconds(FiringRate);
        }

        isFiring = false;
    }

    private void Update()
    {
        if (!isFiring)
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
