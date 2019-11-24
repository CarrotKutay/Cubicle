using UnityEngine;

public class WeaponSlot
{
    private GameObject slot;
    private bool isActiveSlot, holdsWeapon;
    public GameObject Slot { get => slot; set => slot = value; }
    public Transform Transform { get => slot.transform; }
    public bool IsActiveSlot { get => isActiveSlot; set => setActivationTo(value); }
    public bool HoldsWeapon { get => holdsWeapon; set => holdsWeapon = value; }

    private void setActivationTo(bool value)
    {
        slot.SetActive(value);
        isActiveSlot = value;
    }
    public WeaponSlot(Transform parent, LayerMask layer)
    {
        slot = new GameObject();
        slot.name = "WeaponSlot";
        Transform.parent = parent;
        Transform.localPosition = Vector3.back;
        slot.layer = layer;
        isActiveSlot = false;
        holdsWeapon = false;
    }
}