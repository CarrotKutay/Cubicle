using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistanceWeapon : MonoBehaviour
{
    private bool isFiring, equipped;
    private GameObject ptBody;
    protected int ammunition;
    [SerializeField]
    protected int currentAmmunition;
    [SerializeField]
    protected int Damage { get; set; }
    [SerializeField]
    protected float FiringRate { get; set; }
    protected int FiringStrength { get; set; }
    protected Rigidbody Rb { get => rb; set => rb = value; }
    public Vector3 FiringDirection { get => firingDirection; set => firingDirection = value; }
    protected bool IsFiring { get => isFiring; set => isFiring = value; }
    public bool Equipped { get => equipped; set => equipped = value; }
    public GameObject PTBody { get => ptBody; set => ptBody = value; }
    public int CurrentAmmunition { get => currentAmmunition; set => currentAmmunition = value; }
    public int Ammunition { get => ammunition; set => ammunition = value; }

    private Vector3 firingDirection;
    private Rigidbody rb;

    /// <summary>
    /// Restetting CurretAmmunition to the full Ammuntion. Work in progress: 
    /// Update (1) Implement reloading time
    /// Update (2) Implement Visual Cue / Animation
    /// </summary>
    protected void reload()
    {
        CurrentAmmunition = Ammunition;
    }

    /// <summary>
    /// getting a normalized direction from game object to cursor position in ScreenSpace
    /// </summary>
    protected void getCursorPosition()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            FiringDirection = new Vector3(Input.GetAxis("RightJoystickHorizontal1"),-( Input.GetAxis("RightJoystickVertical1")), 0);
            FiringDirection *= 10;
        }
        else
        {
            FiringDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            FiringDirection = new Vector3(
                FiringDirection.x,
                FiringDirection.y,
                0);
        }
    }

    protected void Init(int AmmunitionCount, int Damage, int FiringStrength, float FiringRate)
    {
        Ammunition = AmmunitionCount;
        this.Damage = Damage;
        this.FiringRate = FiringRate;
        this.FiringStrength = FiringStrength;
        gameObject.AddComponent<Rigidbody>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        isFiring = false;
        equipped = false;
    }

    ///<summary>
    /// Method to aim musel of weapon always towards the direction of the firing direction
    ///</summary>
    private void aimWeapon()
    {
        //fixed aim with * 10 the larger vector turns the weapon towards the right direction
        //don't know yet why a smaller vector does not work in this context
        transform.LookAt(firingDirection);
    }

    protected void Update()
    {
        if (equipped)
        {
            getCursorPosition();
            aimWeapon();
            shoot();
        }
    }

    protected virtual void WeaponFired()
    {
        if (CurrentAmmunition > 0)
        {
            addProjectile();
            fireProjectile();
            // Remove Fired Projectile
            CurrentAmmunition = CurrentAmmunition - 1;
        }
    }

    protected virtual void addProjectile()
    {
        //create Projectile Body PTBody
        PTBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PTBody.transform.position = new Vector3(transform.position.x, transform.position.y, 0) + FiringDirection.normalized * 0.5f; // setting a distance (+ FiringDirection.normalized * 0.5f) to the center point of the shooting player to make collision with own projectile not possible
        PTBody.transform.parent = null;
    }
    protected void fireProjectile()
    {
        //Fire Projectile
        Rigidbody rigidbody = PTBody.GetComponent<Rigidbody>();
        rigidbody.mass = 1.5f;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        rigidbody.AddForceAtPosition(FiringDirection.normalized * FiringStrength, PTBody.transform.position, ForceMode.Impulse);
    }

    protected void shoot()
    {

        if (!IsFiring)
        {
            StartCoroutine(checkButtonFired());
        }
    }
    protected IEnumerator checkButtonFired()
    {
        IsFiring = true;

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetAxis("RightTrigger1") > 0 || Input.GetAxis("RightTrigger2") > 0)
        {
            WeaponFired();
            yield return new WaitForSeconds(FiringRate);
        }

        IsFiring = false;
    }

    protected private void OnTransformParentChanged()
    {
        if (transform.parent == null && rb != null)
        {
            isFiring = false;
            equipped = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = true;
        }
        else
        {
            equipped = true;
        }
    }
}
