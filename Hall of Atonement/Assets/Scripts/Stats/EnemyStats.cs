using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyStats : CharacterStats
{
    private float strenghtFromLvl = 1f;
    private float agilityFromLvl = 1f;
    private float masteryFromLvl = 1f;

    private protected override float BaseMovementSpeed { get; } = 3f;
    private protected override float BaseRotationSpeed { get; } = 360f;


    private protected override void Start()
    {
        base.Start();
        GameManager.instance.enemys.Add(gameObject);
    }


    public override void GetExperience(int amount, out bool isLvlUp)
    {
        base.GetExperience((int)(amount / 1.5), out isLvlUp);


        //Если уровень повысился
        if (isLvlUp)
        {
            StatInitialization();
        }
    }


    private protected override void StatInitialization()
    { 
        //Зависимость атрибутов от уровня у врагов

        strength = new Attribute((int)(level.GetLvl() * strenghtFromLvl));
        agility = new Attribute((int)(level.GetLvl() * agilityFromLvl));
        mastery = new Attribute((int)(level.GetLvl() * masteryFromLvl));

        base.StatInitialization();
    }


    public override void Die(CharacterStats killerStats)
    {
        base.Die(killerStats);
        GameManager.instance.enemys.Remove(gameObject);
        Destroy(gameObject);
    }
}
