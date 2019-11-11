using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    private int Health;
    private string Name;
    private float Speed;
    private bool _holdingWeapon;
    private GameObject WeaponSlot1, WeaponSlot2;
    private bool checkingHealth;

    void DropWeapon() { }

    void ThrowWeapon() { }

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
        _holdingWeapon = false;
        checkingHealth = false;
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


}
