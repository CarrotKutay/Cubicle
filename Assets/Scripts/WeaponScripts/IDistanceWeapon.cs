using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDistanceWeapon
{
    IEnumerator reload();
    void getCursorPosition();
    void Init(int AmmunitionCount,
              int Damage,
              int FiringStrength,
              float FiringRate,
              float timeToReload);

}
