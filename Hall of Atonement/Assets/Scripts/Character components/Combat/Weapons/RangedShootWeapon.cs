using System.Collections.Generic;
using UnityEngine;

public class RangedShootWeapon : BaseWeapon, IRanged
{
    public GameObject BulletPrefab;

    [SerializeField] private float shotPower = 20f;

    public void UseWeapon(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool(BulletPrefab, WeaponAttackPoint.transform.position, WeaponAttackPoint.transform.rotation);

        if (bullet != null)
        {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletRb2d.AddForce(WeaponAttackPoint.up * shotPower, ForceMode2D.Impulse); // Weapon.up указывает на направление полета пули!

            bulletScript.BulletInitialization(ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);
        }


        combat.EnableAttackCooldown();
    }
}
