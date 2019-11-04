using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : MonoBehaviour
{
    protected int Ammunition, CurrentAmmunition, Damage;
    protected float FiringRate;
    protected Projectile PT;

    /// <summary>
    /// Restetting CurretAmmunition to the full Ammuntion. Work in progress: 
    /// Update (1) Implement reloading time
    /// Update (2) Implement Visual Cue / Animation
    /// </summary>
    void Reload()
    {
        CurrentAmmunition = Ammunition;
    }

   

    void Init()
    {

    }
}
