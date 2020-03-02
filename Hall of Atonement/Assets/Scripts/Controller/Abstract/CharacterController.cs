﻿using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterController : MonoBehaviour
{
    public Rigidbody2D Rb2D { get; private protected set; }

    public CharacterStats MyStats { get; private protected set; }
    public CharacterCombat Combat { get; private protected set; }
    public Vector2 InputVector { get => inputVector; set => inputVector = value; }
    private Vector2 inputVector = Vector2.zero;


    private protected virtual void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();

        MyStats = GetComponent<CharacterStats>();
        Combat = GetComponent<CharacterCombat>();
    }


    private protected virtual void FixedUpdate()
    {
        Rb2D.velocity = MoveCharacter(inputVector, MyStats.movementSpeed.GetValue(), MyStats.rotationSpeed.GetValue(), MyStats.faceEuler);
    }


    private protected Vector2 MoveCharacter(Vector2 inputVector, float movementSpeed, float rotationSpeed, float faceEuler)
    {
        if (Combat.targetToAttack == null) //Если нет цели атаки, то двигаться ТОЛЬКО лицом вперед
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

         return inputVector;
    }


    //Если есть цель, то игрок должен повернуться лицом к цели, прежде чем начнет атаковть.
    public bool TurnFaceToTarget(GameObject focusTarget, float rotationSpeed, float faceEuler)
    {
        Vector3 difference = (focusTarget.transform.position - transform.position).normalized;

        // Вычисляем кватерион нужного поворота. Вектор forward говорит вокруг какой оси поворачиваться
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: difference);

        // Применяем поворот вокруг оси Z        
        Rb2D.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

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
            Rb2D.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

            //Если достигли нужного поворота к лицевой части существа
            //Если не достигли нужного поворота, то нужно остановиться
            //(transform.rotation.eulerAngles.z + (faceEuler / 2f) >= targetRotation.eulerAngles.z && transform.rotation.eulerAngles.z - (faceEuler / 2f) <= targetRotation.eulerAngles.z)
            return Mathf.Abs(targetRotation.eulerAngles.z - transform.rotation.eulerAngles.z) <= (faceEuler / 2f);
        }
        else //Если InputVector ноль, значит игрок остановился и все Ок
        {
            Rb2D.angularVelocity = 0f;
            return true;
        }
    }
}
