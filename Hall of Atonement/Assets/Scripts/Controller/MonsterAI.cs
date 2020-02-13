using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : EnemyAI
{
    private protected override GameObject SearchingTarget()
    {
        GameObject target = null;

        //Если игрок рядом, то отдать ему приоритет. Если далеко, то сражаться с монстрами
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= LookRadius)
        {
            target = GameManager.instance.player;
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), LookRadius);

            float distance;

            if (target == null)
            {
                distance = float.MaxValue; //Расстояние до цели
            }
            else
            {
                distance = Vector2.Distance(target.gameObject.transform.position, transform.position);
            }

            int enemyCount = 0;

            for (int i = 0; i < colliders.Length; i++)
            {
                //if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) { }
                if (colliders[i].gameObject != gameObject && colliders[i].gameObject.TryGetComponent(out MonsterAI _)) //Если это персонаж или монстер
                {
                    enemyCount++;

                    if (target != null)
                    {
                        float newDistance = Vector2.Distance(colliders[i].gameObject.transform.position, transform.position);
                        if ((newDistance + 2f) < distance)
                        {
                            //Если это не та же самая цель
                            if (colliders[i].gameObject != target)
                            {
                                target = colliders[i].gameObject;

                            }
                            distance = newDistance;
                        }
                    }
                    else
                    {
                        target = colliders[i].gameObject;
                        distance = Vector2.Distance(target.transform.position, transform.position);
                    }
                }
            }

            if (enemyCount == 0)
            {
                target = null;
            }
        }


        //Если цель найдена, идти к ней И атаковать ее, если она достаточно близко

        //плавное сглаживание вектора
        inputVector = Vector2.MoveTowards(inputVector, MoveToTarget(target), 10f * Time.fixedDeltaTime);

        if (target != null)
        {
            combat.SearchingTargetToAttack(target);
        }


        return target;
    }
}
