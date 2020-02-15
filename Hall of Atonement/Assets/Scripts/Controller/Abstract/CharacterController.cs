using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterController : MonoBehaviour
{
    private protected Rigidbody2D rb2D;

    private protected CharacterStats myStats;
    private protected CharacterCombat combat;

    private protected Vector2 inputVector = Vector2.zero;


    private protected virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        myStats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
    }


    private protected virtual void FixedUpdate()
    {
        Movement(inputVector, myStats.movementSpeed.GetValue(), myStats.rotationSpeed.GetValue(), myStats.faceEuler);
    }


    private protected void Movement(Vector2 inputVector, float movementSpeed, float rotationSpeed, float faceEuler)
    {
        if (combat.targetToAttack == null) //Если нет цели атаки, то двигаться ТОЛЬКО лицом вперед
        {
            if (TurnFaceToTarget(inputVector, rotationSpeed, faceEuler)) //Повернулся ли игрок в нужную сторону? См. FaceTarget
            {
                rb2D.velocity = inputVector * movementSpeed;
            }
            else //Если нет, то остановиться
            {
                rb2D.velocity = Vector2.zero;
            }
        }
        else //Если есть цель атаки, то двигаться свободно
        {
            rb2D.velocity = inputVector * movementSpeed;
        }
    }


    //Если есть цель, то игрок должен повернуться лицом к цели, прежде чем начнет атаковть.
    public bool TurnFaceToTarget(GameObject targetAttack, float rotationSpeed, float faceEuler)
    {
        Vector3 difference = (targetAttack.transform.position - transform.position).normalized;

        // Вычисляем кватерион нужного поворота. Вектор forward говорит вокруг какой оси поворачиваться
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: difference);

        // Применяем поворот вокруг оси Z        
        rb2D.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        //Достигли нужного поворота?
        //Если будет разброс:
        return Mathf.Abs(targetRotation.eulerAngles.z - transform.rotation.eulerAngles.z) <= (faceEuler / 12f);

        //Если разброса нет:
        //return Mathf.Approximately(targetRotation.eulerAngles.z,transform.rotation.eulerAngles.z);
    }


    private protected bool TurnFaceToTarget(Vector2 inputVector, float rotationSpeed, float faceEuler)
    {

        Vector3 difference = inputVector.normalized; //Вектор разницы, который определяет, нужно ли игроку повернуться

        if (difference != Vector3.zero) //если InputVector не ноль, значит игрок куда то движется!
        {
            // Вычисляем кватерион нужного поворота. Вектор forward говорит вокруг какой оси поворачиваться
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: difference);

            // Применяем поворот вокруг оси Z
            rb2D.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

            //Если достигли нужного поворота к лицевой части существа
            //Если не достигли нужного поворота, то нужно остановиться
            //(transform.rotation.eulerAngles.z + (faceEuler / 2f) >= targetRotation.eulerAngles.z && transform.rotation.eulerAngles.z - (faceEuler / 2f) <= targetRotation.eulerAngles.z)
            return Mathf.Abs(targetRotation.eulerAngles.z - transform.rotation.eulerAngles.z) <= (faceEuler / 2f);
        }
        else //Если InputVector ноль, значит игрок остановился и все Ок
        {
            rb2D.angularVelocity = 0f;
            return true;
        }
    }
}
