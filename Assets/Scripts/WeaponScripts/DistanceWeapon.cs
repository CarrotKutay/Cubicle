using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistanceWeapon : MonoBehaviour
{
    private bool isFiring, equipped, is_weapon = true;
    protected int Ammunition { get; set; }
    protected int CurrentAmmunition { get; set; }
    protected int Damage { get; set; }
    protected float FiringRate { get; set; }
    protected int FiringStrength { get; set; }
    protected Rigidbody Rb { get => rb; set => rb = value; }
    public Vector3 FiringDirection { get => firingDirection; set => firingDirection = value; }
    protected bool IsFiring { get => isFiring; set => isFiring = value; }
    public bool is_Weapon { get => is_weapon; }
    public bool Equipped { get => equipped; set => equipped = value; }

    private Vector3 firingDirection;
    private Rigidbody rb;

    /// <summary>
    /// Restetting CurretAmmunition to the full Ammuntion. Work in progress: 
    /// Update (1) Implement reloading time
    /// Update (2) Implement Visual Cue / Animation
    /// </summary>
    public void Reload()
    {
        CurrentAmmunition = Ammunition;
    }

    /// <summary>
    /// getting a normalized direction from game object to cursor position in ScreenSpace
    /// </summary>
    protected void getCursorPosition()
    {
        FiringDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        FiringDirection = new Vector3(
            FiringDirection.x,
            FiringDirection.y,
            0);
        //firingDirection = FiringDirection.normalized;
    }

    protected void getJoystickDirection()
    {

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

    protected virtual void shoot() { }

    protected private void OnTransformParentChanged()
    {

        if (transform.parent == null)
        {
            isFiring = false;
            equipped = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = true;
        }
    }
}
