using System.Collections.Generic;
using UnityEngine;

public class RangedShootCombat : MonoBehaviour, IRanged
{
    Transform IAttacker.AttackPoint => weapon;

    public Transform weapon;

    public GameObject BulletPrefab;

    private float bullerForce = 20f;

    public void Attack(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        print(gameObject.name + " использует пушку!");

        //GameObject bullet = Instantiate(BulletPrefab, weapon.position, weapon.rotation);


        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = weapon.transform.position;
            bullet.transform.rotation = weapon.transform.rotation;
            //bullet.SetActive(true);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletRb2d.AddForce(weapon.up * bullerForce, ForceMode2D.Impulse);

            bulletScript.BulletInitialization(gameObject, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, attackModifiers);
        }
    }
}
