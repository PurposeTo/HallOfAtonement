using System.Collections.Generic;
using UnityEngine;

public class RangedLaserWeapon : MonoBehaviour, IRanged
{
    public Transform weapon;

    private Ray2D ray;

    Transform IAttacker.AttackPoint => weapon;


    void IAttacker.Attack(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        print(gameObject.name + " использует дальнюю атаку!");

        RaycastHit2D hit;
        
        Quaternion rotation = transform.rotation;
        ray = new Ray2D(weapon.position, rotation * Vector2.up);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 0.6f);

        //Выстрелить
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        //Пуля попала во что то
        //for (int i = 0; i < hit.Length; i++)
        //{
        if (hit.collider.gameObject != gameObject)
        {
            Debug.Log(gameObject.name + " попал в: " + hit.collider.gameObject.name);

            //Это что то имеет Статы?
            if (hit.collider.gameObject.TryGetComponent(out UnitStats targetStats))
            {
                combat.DoDamage(targetStats, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, attackModifiers);
            }
        }
        //}
    }
}
