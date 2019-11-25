using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private int Health;
    public int health { get => Health; set => Health = value; }
    private string Name;
    private float Speed;
    private LayerMask personalLayer;
    /// <summary>
    /// we are providing the option of holding two weapons which are interchangeable for the player to choose from and hold while carrying
    /// <param name="WeaponSlot1"> </param>
    /// <param name="WeaponSlot2"> </param>
    /// </summary>
    public WeaponSlot WeaponSlot1;
    public WeaponSlot WeaponSlot2;
    /// <summary>
    /// ActiveWeapon will be the Weapon the player performs actions with, it is interchangable bewteen "WeaponSlot1" and "WeaponSlot2"
    /// </summary>
    public GameObject getActiveWeapon
    {
        get
        {
            if (WeaponSlot1.IsActiveSlot && WeaponSlot1.HoldsWeapon)
            {
                return WeaponSlot1.Transform.GetChild(0).gameObject;
            }
            else if (WeaponSlot2.IsActiveSlot && WeaponSlot2.HoldsWeapon)
            {
                return WeaponSlot2.Transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    private bool checkingHealth;
    public LayerMask PersonalLayer { get => personalLayer; set => personalLayer = value; }

    void equipWeapon(GameObject Weapon)
    {
        Weapon.layer = personalLayer;
        Weapon.GetComponent<Rigidbody>().useGravity = false;

        if (!WeaponSlot1.HoldsWeapon) // equip weapon on slot 1 if empty
        {
            Weapon.transform.parent = WeaponSlot1.Transform;
            WeaponSlot1.HoldsWeapon = true;
            if (!WeaponSlot2.HoldsWeapon) { WeaponSlot1.IsActiveSlot = true; }
        }
        else if (!WeaponSlot2.HoldsWeapon) // equip weapon on slot 2 if empty
        {
            Weapon.transform.parent = WeaponSlot2.Transform; ;
            WeaponSlot2.HoldsWeapon = true;
            if (!WeaponSlot1.HoldsWeapon) { WeaponSlot2.IsActiveSlot = true; }
        }
        Weapon.transform.localPosition = Vector3.zero;
        Weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

    }

    ///<summary>
    /// Method to change the active Weapon from either one of the Weapon Slots to the other
    ///</summary>
    void changeActiveWeapon()
    {
        if (WeaponSlot1.IsActiveSlot)
        {
            WeaponSlot1.IsActiveSlot = false;
            WeaponSlot2.IsActiveSlot = true;
        }
        else if (WeaponSlot2.IsActiveSlot)
        {
            WeaponSlot1.IsActiveSlot = true;
            WeaponSlot2.IsActiveSlot = false;
        }
    }

    public void pickUp(GameObject obj)
    {
        equipWeapon(obj);
    }
    void DropWeapon()
    {
        removeWeaponFromPlayer();
    }

    private void removeWeaponFromPlayer()
    {
        WeaponSlot weaponSlot = WeaponSlot1.IsActiveSlot ? WeaponSlot1 : WeaponSlot2;
        if (weaponSlot.HoldsWeapon)
        {
            Transform dropWeapon = weaponSlot.Transform.GetChild(0);
            dropWeapon.transform.parent = null;
            dropWeapon.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            weaponSlot.IsActiveSlot = false;
            weaponSlot.Transform.DetachChildren();
            weaponSlot.HoldsWeapon = false;
        }
    }

    public void throwWeapon()
    {
        if (isThrowableObject())
        {
            removeWeaponFromPlayer();
        }
    }

    private bool isThrowableObject()
    {
        bool slot1 = WeaponSlot1.IsActiveSlot && WeaponSlot1.HoldsWeapon, slot2 = WeaponSlot2.IsActiveSlot && WeaponSlot2.HoldsWeapon;
        return slot1 || slot2;
    }

    ///<summary>
    /// Coroutine checking if <see cref="this.Health"/>  is below or at 0
    ///</summary>
    private IEnumerator healthcare()
    {
        checkingHealth = true;

        if (iAmDead())
        {
            Debug.Log("You died");
            Destroy(this.gameObject);
        }

        yield return new WaitForSeconds(0.5f);
        checkingHealth = false;
    }

    private bool iAmDead()
    {
        return Health <= 0;
    }

    /// <summary>
    /// When Cube-Characters health gets influenced by outside force this method is called to update its value.
    /// UpdateValue is the parameter by which the health value will change. The characters size will also change according to the health percentage it posesses.
    /// </summary>
    /// <param name="Updatevalue"></param>
    public void UpdateHealth(int UpdateValue)
    {
        Debug.Log("Updated health by " + UpdateValue);
        Health += UpdateValue;
        float newScale = transform.localScale.x + (UpdateValue / 200f);
        float scaleSize = newScale < 0.25f ? 0.25f : newScale;
        transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
    }

    /// <summary>
    /// initialising character with starting values
    /// </summary>
    private void InitCharacter()
    {
        Health = 100;
        Speed = 1;
        personalLayer = gameObject.layer;
        initSlots();
        checkingHealth = false;
        gameObject.layer = personalLayer;
        this.name = "Player";
    }

    private void initSlots()
    {
        WeaponSlot1 = new WeaponSlot(transform, personalLayer);
        WeaponSlot2 = new WeaponSlot(transform, personalLayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkingHealth) StartCoroutine(healthcare());
        checkReload();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Arena"))
        {
            if (Input.GetButtonDown("PickUp"))
            {
                pickUp(other.gameObject);
            }
        }
    }

    private void checkReload()
    {
        if (Input.GetButtonDown("Reload") || Input.GetAxis("ReloadGP1") > 0 || Input.GetAxis("ReloadGP2") > 0) 
        {
            getActiveWeapon.SendMessage("reload");
        }
    }
}
