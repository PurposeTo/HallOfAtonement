using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TestGunCombat GunCombat;

    public CharacterCombat combat;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("~Пуля столкнулась с: " + collision.gameObject.name);
        GunCombat.BulletDoDamage(gameObject, collision.gameObject, combat);
    }
}
