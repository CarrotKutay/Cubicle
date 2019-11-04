using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private int Health;
    private string Name;
    private float Speed;
    private bool _holdingWeapon;
    private GameObject WeaponSlot1, WeaponSlot2;

    void DropWeapon() { }

    void ThrowWeapon() { }

    void GettingHit(int ByValue)
    {
        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    /// <summary>
    /// When Cube-Characters health gets influenced by outside force this method is called to update its value.
    /// UpdateValue is the parameter by which the health value will change. The characters size will also change according to the health percentage it posesses.
    /// </summary>
    /// <param name="Updatevalue"></param>
    void UpdateHealth(int UpdateValue)
    {
        Health += UpdateValue;
        float scaleSize = transform.localScale.x + UpdateValue/2f;
        transform.localScale.Set(scaleSize, scaleSize, scaleSize);
    }

    /// <summary>
    /// initialising character with starting values
    /// </summary>
    private void InitCharacter()
    {
        Health = 100;
        Speed = 1;
        _holdingWeapon = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
