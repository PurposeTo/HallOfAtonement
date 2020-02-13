﻿using UnityEngine;

public class RangedCombat : MonoBehaviour, IAttacker
{
    private protected CharacterCombat combat;

    public Transform weapon;

    Ray2D ray;

    public void Attack()
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
                if (hit.collider.gameObject.GetComponent<UnitStats>())
                {
                    combat.DoDamage(hit.collider.gameObject.GetComponent<UnitStats>());
            }
        }
        //}
    }


    private protected virtual void Start()
    {
        combat = gameObject.GetComponent<CharacterCombat>();
    }
}