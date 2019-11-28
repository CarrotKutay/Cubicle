using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistanceWeapon : MonoBehaviour, IDistanceWeapon
{
    private bool isFiring, equipped;
    private GameObject ptBody;
    private string fireButton;
    protected int ammunition;
    [SerializeField]
    protected int currentAmmunition;
    [SerializeField]
    protected int Damage { get; set; }
    [SerializeField]
    protected float FiringRate { get; set; }
    [SerializeField]
    protected int FiringStrength { get; set; }
    protected float timeToReload;
    protected Rigidbody Rb { get => rb; set => rb = value; }
    public Vector3 FiringDirection { get => firingDirection; set => firingDirection = value; }
    public bool IsFiring { get => isFiring; set => isFiring = value; }
    public bool Equipped { get => equipped; set { equipped = value; } }
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
    public IEnumerator reload()
    {
        GameObject reloadBar = GameObject.FindGameObjectWithTag("ReloadBar");
        if (reloadBar != null)
        {
            if (reloadBar.transform.GetChild(0).TryGetComponent<ReloadProgressBar>(out ReloadProgressBar bar)) { bar.TimeToReload = timeToReload; }
        }
        yield return new WaitForSeconds(timeToReload);
        CurrentAmmunition = Ammunition;
    }

    /// <summary>
    /// getting a normalized direction from game object to cursor position in ScreenSpace
    /// </summary>
    public void getCursorPosition()
    {
        GameObject player = transform.parent.parent.gameObject;


        if (player.TryGetComponent<GamePadController>(out GamePadController controller))
        {
            FiringDirection = new Vector3(Input.GetAxis("RightJoystickHorizontal" + controller.ControllerNumber), -(Input.GetAxis("RightJoystickVertical" + controller.ControllerNumber)), 0);
            FiringDirection *= 10;
        }
        else
        {
            FiringDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
            FiringDirection = new Vector3(
                FiringDirection.x,
                FiringDirection.y,
                0);
        }
    }

    public void Init(int AmmunitionCount, int Damage, int FiringStrength, float FiringRate, float timeToReload)
    {
        Ammunition = AmmunitionCount;
        this.Damage = Damage;
        this.FiringRate = FiringRate;
        this.FiringStrength = FiringStrength;
        this.timeToReload = timeToReload;
        gameObject.AddComponent<Rigidbody>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        isFiring = false;
        equipped = false;
        gameObject.tag = "Weapon";
    }

    private void setInputs()
    {
        if (transform.parent.parent.gameObject.TryGetComponent<GamePadController>(out GamePadController cgp))
        {
            fireButton = "RightTrigger" + cgp.ControllerNumber;
        }
        else if (transform.parent.parent.gameObject.TryGetComponent<PlayerController>(out PlayerController _))
        {
            fireButton = "0";
        }

    }

    private bool checkForGamePad()
    {
        return transform.parent.parent.gameObject.TryGetComponent<GamePadController>(out GamePadController _);
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
        if (checkForGamePad())
        {
            if (Input.GetAxis(fireButton) > 0 || Input.GetAxis(fireButton) > 0)
            {
                WeaponFired();
                yield return new WaitForSeconds(FiringRate);
            }
        }
        else
        {
            if (Input.GetMouseButton(int.Parse(fireButton)) || Input.GetMouseButtonDown(int.Parse(fireButton)))
            {
                WeaponFired();
                yield return new WaitForSeconds(FiringRate);
            }
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
            setInputs();
        }
    }
}
