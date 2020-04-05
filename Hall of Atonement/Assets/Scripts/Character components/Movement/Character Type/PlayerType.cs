using UnityEngine;

public class PlayerType : CharacterType
{
    private GameObject mainCamera;

    private float cameraSize = 5f; //Размер ортографической камеры

    private Vector2 attackZone = Vector2.zero; //Дальность прицеливания. Зависит от дальности камеры


    private void Start()
    {
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


    public override GameObject SearchingTarget()
    {
        GameObject target = null;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackZone, 0f);

        float distance = float.MaxValue;

        for (int i = 0; i < colliders.Length; i++)
        {
            //if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) { }
            if (colliders[i].gameObject.TryGetComponent(out EnemyAI _)) //Если это враг, то сделать его целью
            {

                if (target != null)
                {
                    float newDistance = Vector2.Distance(colliders[i].gameObject.transform.position, transform.position);
                    if (newDistance < distance)
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

        return target;
    }
}
