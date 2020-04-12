using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }

    private protected bool isEnabled = true;

    public void DisableMovement()
    {
        isEnabled = false;
    }


    public void EnableMovement()
    {
        isEnabled = true;
    }


    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();
    }


    public void MoveCharacter(Vector2 inputVector)
    {
        float movementSpeed = CharacterPresenter.MyStats.movementSpeed.GetValue();
        float rotationSpeed = CharacterPresenter.MyStats.rotationSpeed.GetValue();
        float faceEuler = CharacterPresenter.MyStats.faceEuler;

        GameObject focusTarget = CharacterPresenter.Combat.GetTargetToAttack();

        if (isEnabled)
        {

            if (focusTarget == null) //Если нет цели атаки, то двигаться ТОЛЬКО лицом вперед
            {
                if (TurnFaceToTarget(inputVector, rotationSpeed, faceEuler)) //Повернулся ли персонаж в нужную сторону? См. FaceTarget
                {
                    inputVector *= movementSpeed;
                }
                else //Если нет, то стоять до тех пор, пока не повернется
                {
                    inputVector = Vector2.zero;
                }
            }
            else //Если есть цель атаки, то двигаться свободно
            {
                inputVector *= movementSpeed;
            }
        }
        else
        {
            inputVector = Vector2.zero;
        }

        CharacterPresenter.Rb2d.velocity = inputVector;
        //return inputVector;
    }


    //Если есть цель, то игрок должен повернуться лицом к цели, прежде чем начнет атаковть.
    public bool TurnFaceToTarget(GameObject focusTarget, float rotationSpeed, float faceEuler)
    {
        if (isEnabled)
        {
            if (focusTarget != null)
            {
                Vector3 difference = (focusTarget.transform.position - transform.position).normalized;

                // Вычисляем кватернион нужного поворота. Вектор forward говорит вокруг какой оси поворачиваться
                Quaternion necessaryRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: difference);

                // Применяем поворот вокруг оси Z        
                CharacterPresenter.Rb2d.MoveRotation(Quaternion.RotateTowards(transform.rotation, necessaryRotation, rotationSpeed * Time.fixedDeltaTime));

                //Достигли нужного поворота?
                //Если будет разброс:
                return IsCorrectTurn(necessaryRotation, faceEuler / 12f);

                //Если разброса нет:
                //return Mathf.Approximately(targetRotation.eulerAngles.z,transform.rotation.eulerAngles.z);
            }
            else
            {
                return true;
            }
        }
        else return false;
        
    }


    private protected bool TurnFaceToTarget(Vector2 inputVector, float rotationSpeed, float faceEuler)
    {
        Vector3 difference = inputVector.normalized; //Вектор разницы, который определяет, нужно ли игроку повернуться

        if (difference != Vector3.zero) //если InputVector не ноль, значит игрок куда то движется!
        {
            // Вычисляем кватерион нужного поворота. Вектор forward говорит вокруг какой оси поворачиваться
            Quaternion necessaryRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: difference);

            // Применяем поворот вокруг оси Z
            CharacterPresenter.Rb2d.MoveRotation(Quaternion.RotateTowards(transform.rotation, necessaryRotation, rotationSpeed * Time.fixedDeltaTime));

            //Если достигли нужного поворота к лицевой части существа
            //Если не достигли нужного поворота, то нужно остановиться
            //(transform.rotation.eulerAngles.z + (faceEuler / 2f) >= targetRotation.eulerAngles.z && transform.rotation.eulerAngles.z - (faceEuler / 2f) <= targetRotation.eulerAngles.z)
            return IsCorrectTurn(necessaryRotation, faceEuler / 2f);
        }
        else //Если InputVector ноль, значит игрок остановился и все Ок
        {
            CharacterPresenter.Rb2d.angularVelocity = 0f;
            return true;
        }
    }


    //public bool IsCorrectTurn(GameObject focusTarget) // Данный метод использовать только извне!
    //{
    //    if (focusTarget != null)
    //    {
    //        Vector3 difference = (focusTarget.transform.position - transform.position).normalized;

    //        // Вычисляем кватернион нужного поворота. Вектор forward говорит вокруг какой оси поворачиваться
    //        Quaternion necessaryRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: difference);

    //        float faceEuler = CharacterPresenter.MyStats.faceEuler;

    //        return Mathf.Abs(necessaryRotation.eulerAngles.z - transform.rotation.eulerAngles.z) <= (faceEuler / 12f);
    //    }
    //    else return true;
    //}


    private bool IsCorrectTurn(Quaternion necessaryRotation, float Euler)
    {
        return Mathf.Abs(necessaryRotation.eulerAngles.z - transform.rotation.eulerAngles.z) <= Euler;
    }
}
