using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : MonoBehaviour
{
    protected int Ammunition { get; set; }
    protected int CurrentAmmunition { get; set; }
    protected int Damage { get; set; }
    protected float FiringRate { get; set; }
    protected int FiringStrength { get; set; }
    protected List<Projectile> PT;
    protected Vector3 firingDirection;

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
        firingDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        firingDirection.z = 0;
        firingDirection.Normalize();
    }
    protected void Init(int AmmunitionCount, int Damage, int FiringStrength, float FiringRate)
    {
        Ammunition = AmmunitionCount;
        this.Damage = Damage;
        this.FiringRate = FiringRate;
        this.FiringStrength = FiringStrength;
        Reload();
    }
}
