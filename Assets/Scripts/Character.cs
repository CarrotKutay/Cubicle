using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    private int Health;
    private string Name;
    private float Speed;
    ///<summary>
    /// Boolean Array to indicate if Weapon is hold in Slot 1 (holdingWeapon[0]) or Slot 2 (holdingWeapon[1]). Might be opsolete in further updates -> possible to perform Weapon actions in a class "Slot"
    /// <param name="holdingWeapon"></param>
    ///</summary>
    private bool[] holdingWeapon;
    private LayerMask personalLayer;
    /// <summary>
    /// we are providing the option of holding two weapons which are interchangeable for the player to choose from and hold while carrying
    /// <param name="WeaponSlot1"> </param>
    /// <param name="WeaponSlot2"> </param>
    /// </summary>
    private GameObject WeaponSlot1, WeaponSlot2;
    /// <summary>
    /// ActiveWeapon will be the Weapon the player performs actions with, it is interchangable bewteen "WeaponSlot1" and "WeaponSlot2"
    /// <param name="ActiveWeapon"> </param>
    /// </summary>
    private GameObject activeWeapon;
    private bool checkingHealth;

    public GameObject ActiveWeapon { get => activeWeapon; set => activeWeapon = value; }
    public LayerMask PersonalLayer { get => personalLayer; set => personalLayer = value; }

    void equipWeapon(GameObject Weapon)
    {
        Weapon.GetComponent<DistanceWeapon>().Equipped = true;
        Weapon.GetComponent<Rigidbody>().useGravity = false;

        if (WeaponSlot1 == null) // equip weapon on slot 1 if empty
        {
            Weapon.transform.parent = WeaponSlot1.transform;
            holdingWeapon[0] = true;
            if (!holdingWeapon[1]) { ActiveWeapon = WeaponSlot1; }
        }
        else if (WeaponSlot2 == null) // equip weapon on slot 2 if empty
        {
            Weapon.transform.parent = WeaponSlot2.transform; ;
            holdingWeapon[1] = true;
            if (!holdingWeapon[0]) { ActiveWeapon = WeaponSlot2; }
        }
        else
        {
            if (WeaponSlot1.Equals(ActiveWeapon))
            {
                Weapon.transform.parent = WeaponSlot1.transform;
            }
            else
            {
                Weapon.transform.parent = WeaponSlot2.transform;
            }
        }
        Weapon.GetComponent<DistanceWeapon>().HeldIn = PersonalLayer;
        Weapon.transform.localPosition = Vector3.zero;
        Weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

    }
    ///<summary>
    /// Method to set active weapon to the given parameter. Weapon should only be set active via this method.
    /// <param name="Weapon"></param>
    ///</summary>
    private void setActiveWeapon(GameObject Weapon)
    {

    }

    ///<summary>
    /// Method to change the active Weapon from either one tóf the Weapon Slots to the other
    ///</summary>
    void changeActiveWeapon()
    {
        if (ActiveWeapon == WeaponSlot1)
        {
            WeaponSlot1.gameObject.SetActive(false);
            WeaponSlot2.gameObject.SetActive(true);
            ActiveWeapon = WeaponSlot2;
        }
        else if (ActiveWeapon == WeaponSlot2)
        {
            WeaponSlot1.gameObject.SetActive(true);
            WeaponSlot2.gameObject.SetActive(false);
            ActiveWeapon = WeaponSlot1;
        }
    }

    public void pickUp(GameObject obj)
    {

        if (obj.GetComponent<DistanceWeapon>().IS_WEAPON)
        {
            equipWeapon(obj);
        }
    }
    void DropWeapon() { }

    void ThrowWeapon() { }

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
        initSlots();
        holdingWeapon = new bool[2];
        holdingWeapon[1] = false;
        holdingWeapon[0] = false;
        checkingHealth = false;
        PersonalLayer = LayerMask.GetMask("Player1");
        gameObject.layer = personalLayer.value;
    }

    private void initSlots()
    {
        WeaponSlot1 = new GameObject();
        WeaponSlot2 = new GameObject();
        WeaponSlot1.name = "Slot1";
        WeaponSlot2.name = "Slot2";
        WeaponSlot1.transform.parent = transform;
        WeaponSlot2.transform.parent = transform;
        WeaponSlot1.transform.localPosition = Vector3.back;
        WeaponSlot2.transform.localPosition = Vector3.back;
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
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer != LayerMask.GetMask("Arena"))
        {
            if (Input.GetButtonDown("PickUp"))
            {
                pickUp(other.gameObject);
            }
        }
    }

}
