using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : MonoBehaviour
{
    private bool isFiring, equipped, iS_WEAPON = true;
    protected int Ammunition { get; set; }
    protected int CurrentAmmunition { get; set; }
    protected int Damage { get; set; }
    protected float FiringRate { get; set; }
    protected int FiringStrength { get; set; }
    protected Rigidbody Rb { get => rb; set => rb = value; }
    protected Vector3 FiringDirection { get => firingDirection; set => firingDirection = value; }
    protected bool IsFiring { get => isFiring; set => isFiring = value; }
    public bool IS_WEAPON { get => iS_WEAPON; }
    public bool Equipped { get => equipped; set => equipped = value; }

    private Vector3 firingDirection;
    private Rigidbody rb;

    /// <summary>
    /// Restetting CurretAmmunition to the full Ammuntion. Work in progress: 
    /// Update (1) Implement reloading time
    /// Update (2) Implement Visual Cue / Animation
    /// </summary>
    private void Reload()
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
        firingDirection = FiringDirection.normalized;
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
        Reload();
    }

    ///<summary>
    /// Method to aim musel of weapon always towards the direction of the firing direction
    ///</summary>
    private void aimWeapon()
    {
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
}
