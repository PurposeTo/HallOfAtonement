using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerCombat : CharacterCombat
{
    private GameObject mainCamera;

    [HideInInspector] public bool isWantToAttack = false;

    private float cameraSize = 5f; //Размер ортографической камеры

    private Vector2 attackZone = Vector2.zero; //Дальность прицеливания. Зависит от дальности камеры


    private protected override void Start()
    {
        base.Start();

        myStats = (PlayerStats)myStats;
        controller = (PlayerController)controller;

        mainCamera = Camera.main.gameObject;
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, cameraRange);
        mainCamera.GetComponent<Camera>().orthographicSize = cameraSize;
        //Вычисляем соотношение сторон экрана
        float screenRatio = (float)Screen.width / (float)Screen.height;
        screenRatio = (float)System.Math.Round(screenRatio, 3);
        //Вычисляем высоту экрана
        //float screenHeight = Mathf.Abs(Mathf.Tan(mainCamera.GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad / 2f) * cameraRange * 2f);
        float screenHeight = cameraSize * 2;
        //Вычисляем ширину экрана
        float screenWidth = screenHeight * screenRatio;
        attackZone = new Vector2(screenWidth, screenHeight);
    }


    private protected virtual void FixedUpdate()
    {
        if (isWantToAttack)
        {
            //Искать цель
            SearchingTargetToAttack(targetToAttack);
        }
    }


    private protected override void Update()
    {
        base.Update();


        if (Input.GetKey(KeyCode.Space)) //Если нажата кнопка атаки
        {
            isWantToAttack = true;
        }
        else //Если НЕ нажата кнопка атаки
        {
            isWantToAttack = false;
            targetToAttack = null;
        }
    }


    public override void SearchingTargetToAttack(GameObject target)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackZone, 0f);

        float distance;

        if (target == null)
        {
            distance = float.MaxValue; //Расстояние от персонажа до цели
        }
        else
        {
            distance = Vector2.Distance(target.gameObject.transform.position, transform.position);
        }

        int enemyCount = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            //if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) { }
            if (colliders[i].gameObject.TryGetComponent(out EnemyAI _)) //Если это враг, то сделать его целью
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

        targetToAttack = target;
        Attack();
    }
}
