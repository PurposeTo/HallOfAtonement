using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public Transform WeaponAttackPoint => gameObject.transform;
}
