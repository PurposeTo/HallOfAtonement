using System.Collections.Generic;
using UnityEngine;

public class RangedShootWeapon : BaseWeapon, IRanged
{
    public GameObject BulletPrefab;

    private float bullerForce = 20f;

    public void UseWeapon(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        print(gameObject.name + " использует пушку!");

        //GameObject bullet = Instantiate(BulletPrefab, weapon.position, weapon.rotation);


        GameObject bullet = ObjectPooler.Instance.SpawnFromPool(BulletPrefab, WeaponAttackPoint.transform.position, WeaponAttackPoint.transform.rotation);

        if (bullet != null)
        {
            //bullet.transform.position = weapon.transform.position;
            //bullet.transform.rotation = weapon.transform.rotation;
            //bullet.SetActive(true);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletRb2d.AddForce(WeaponAttackPoint.up * bullerForce, ForceMode2D.Impulse); // Weapon.up указывает на направление полета пули!

            bulletScript.BulletInitialization(ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);
        }


        combat.EnableAttackCooldown();
    }
}
