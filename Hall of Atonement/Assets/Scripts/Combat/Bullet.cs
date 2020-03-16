using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour//, IPooledObject
{
    public RangedGunCombat RangedGunCombat;

    public CharacterCombat combat;


    //void IPooledObject.OnObjectSpawn()
    //{
    //    bulletDoDamage = RangedGunCombat.BulletDoDamage;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Пуля столкнулась с: " + collision.gameObject.name);

        RangedGunCombat.BulletDoDamage(gameObject, collision.gameObject, combat);
    }
}
