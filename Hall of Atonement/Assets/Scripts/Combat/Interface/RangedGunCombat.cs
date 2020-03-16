using UnityEngine;

public class RangedGunCombat : MonoBehaviour, IRanged
{
    Transform IAttacker.AttackPoint => weapon;

    public Transform weapon;

    public GameObject BulletPrefab;

    private float bullerForce = 20f;

    public void Attack(CharacterCombat combat)
    {
        print(gameObject.name + " использует пушку!");

        //GameObject bullet = Instantiate(BulletPrefab, weapon.position, weapon.rotation);


        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = weapon.transform.position;
            bullet.transform.rotation = weapon.transform.rotation;
            bullet.SetActive(true);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.RangedGunCombat = this;
            bulletScript.combat = combat;

            Rigidbody2D bulletRb2d = bullet.GetComponent<Rigidbody2D>();
            bulletRb2d.AddForce(weapon.up * bullerForce, ForceMode2D.Impulse);
        }
    }


    public void BulletDoDamage(GameObject mySelf, GameObject targetObject, CharacterCombat combat)
    {
        if (targetObject != gameObject)
        {
            Debug.Log(gameObject.name + " попал в: " + targetObject.name);

            //Это что то имеет Статы?
            if (targetObject.TryGetComponent(out UnitStats targetStats))
            {
                combat.DoDamage(targetStats);
            }

            mySelf.SetActive(false);
            //Destroy(mySelf);
        }
    }
}
