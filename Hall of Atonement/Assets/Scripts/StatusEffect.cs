using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    //Добавить эффект в статус бар (в список)
    //Функция "при добавлении"
    //В статус баре: каждый апдейт проходиться циклом по списку и выполнять функцию Action (что должно происходить)
    //Если способность активная - то должно быть время действия
    //У способности есть аура?


    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract void OnAdd();
    public abstract void Action(CharacterStats stats);
}


public abstract class PassiveEffect : StatusEffect
{ }


public abstract class ActiveEffect : StatusEffect
{
    public abstract float DurationTime { get; set; }
}


    public class PoisonEffect : ActiveEffect
{
    public override string Name { get; } = "Poison";
    public override string Description { get; } = "You are poisoned and take damage!";
    public override float DurationTime { get; set; } = 10f;

    private float poisonDamage = 4f;

    public override void OnAdd()
    {
        print("Ouch! It's poison!");
    }

    public override void Action(CharacterStats stats)
    {
        stats.TakeDamage(null, poisonDamage * Time.deltaTime);
    }
}
