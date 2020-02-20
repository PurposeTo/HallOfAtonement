using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public abstract class CharacterStats : UnitStats
{
    public HealthBar healthBar;
    public LvlBar lvlBar;

    public LevelSystem level = new LevelSystem();
    public Attribute strength = new Attribute();
    public Attribute agility = new Attribute();
    public Attribute mastery = new Attribute();

    //Зависимость статов от Силы
    private readonly float hpForStrenght = 20f;
    private readonly float hpRegenForStrenght = 0.1f;
    private readonly float movementSpeedForStrenght = -0.15f;
    private readonly float rotationSpeedForStrenght = -20f;
    private readonly float attackDamageForStrenght = 3f;
    private readonly float attackSpeedForStrenght = -0.05f;


    //Зависимость статов от Ловковсти
    private readonly float movementSpeedForAgility = 0.17f;
    private readonly float rotationSpeedForAgility = 25f;
    private readonly float attackDamageForAgility = 0.75f;
    private readonly float attackSpeedForAgility = 0.15f;
    private readonly float armorForAgility = 0.5f;
    private readonly float evasionForAgility = 0.05f;


    //Зависимость статов от Мастерства
    private readonly float evasionForMastery = 0.1f;
    private readonly float criticalChanceForMastery = 1f;
    private readonly float criticalMultiplierForMastery = 10f;
    private readonly float experieneMultiplierForMastery = 0.1f;
    //private readonly float улучшение баффов



    private protected override float BaseMaxHealthPoint { get; } = 100f; //базовое значение максимального кол-ва здоровья
    private protected virtual float BaseHealthPointRegen { get; } = 0.2f; //базовое значение регенерации здоровья
    //public float CurrentHealthPoint { get; private protected set; }

    private readonly float minMovementSpeed = 1.6f; //минимальное значение скорости
    private readonly float maxMovementSpeed = 30f; //максимальное значение скорости
    private protected virtual float BaseMovementSpeed { get; } = 10f; //базовое значение скорости

    private readonly float minRotationSpeed = 180f; //минимальное значение скорости поворота
    private protected virtual float BaseRotationSpeed { get; } = 1080f; //базовое значение скорости поворота

    private protected virtual float BaseAttackDamage { get; } = 10f; //базовое значение атаки
    private readonly float maxAttackSpeed = 50f; //максимальное значение скорости атаки
    private protected virtual float BaseAttackSpeed { get; } = 0.5f; //базовое значение скорости атаки

    private readonly float minCriticalMultiplier = 110f; //минимальное значение множителя критической атаки
    private protected virtual float BaseCriticalMultiplier { get; } = 2f; //базовое значение множителя критической атаки.
    private readonly float minCriticalChance = 0f; //минимальное значение скорости поворот
    private readonly float maxCriticalChance = 1.1f; //минимальное значение скорости поворот.
    private protected virtual float BaseCriticalChance { get; } = 1f; //базовое значение скорости поворот

    private readonly float minEvasionChance = 0f; //минимальное значение скорости уворота
    private readonly float maxEvasionChance = 70f; //минимальное значение скорости уворота\


    //public Stat maxHealthPoint;
    public Stat healthPointRegen;
    public Stat movementSpeed;
    public Stat rotationSpeed;
    public readonly float faceEuler = 60f; //Угол лицевой стороны существа. Все действия игрок совершает лицом к объекту действий!
    public Stat attackDamage;

    public enum ContainerDamageTypes
    {
        PhysicalDamage,
        FireDamage,
        IceDamage,
        BleedingDamage,
        PoisonDamage
    }

    public ContainerDamageTypes UnitDamageType;

    public virtual DamageType DamageType { get; private protected set; }

    public Stat attackSpeed; //(Кол-во атак в секунду)
    public Stat criticalMultiplier; //Крит. множитель атаки
    public Stat criticalChance;
    //public Stat armor; //Нет базового значения
    public Stat evasionChance; //Нет базового значения


    private protected virtual void Start()
    {
        ChangeDamageType(UnitDamageType);
    }


    private protected virtual void FixedUpdate()
    {
        Healing(healthPointRegen.GetValue() * Time.fixedDeltaTime);
    }


    //Инициализация статов в зависимости от атрибутов
    private protected override void StatInitialization()
    {
        //Разделить отдельно для ловкости, силы и мастерства!
        maxHealthPoint = new Stat(BaseMaxHealthPoint + (strength.GetValue() * hpForStrenght));
        healthPointRegen = new Stat(BaseHealthPointRegen + (strength.GetValue() * hpRegenForStrenght));


        movementSpeed = new Stat(BaseMovementSpeed +
            (strength.GetValue() * movementSpeedForStrenght) + (agility.GetValue() * movementSpeedForAgility), minMovementSpeed, maxMovementSpeed);
        rotationSpeed = new Stat(BaseRotationSpeed +
            (strength.GetValue() * rotationSpeedForStrenght) + (agility.GetValue() * rotationSpeedForAgility), minRotationSpeed);

        attackDamage = new Stat(BaseAttackDamage +
            (strength.GetValue() * attackDamageForStrenght) + (agility.GetValue() * attackDamageForAgility));

        attackSpeed = new Stat(BaseAttackSpeed +
            (strength.GetValue() * attackSpeedForStrenght) + (agility.GetValue() * attackSpeedForAgility), 0.01f, maxAttackSpeed);
        criticalMultiplier = new Stat(BaseCriticalMultiplier + (mastery.GetValue() * criticalMultiplierForMastery), minCriticalMultiplier);
        criticalChance = new Stat(BaseCriticalChance + (mastery.GetValue() * criticalChanceForMastery), minCriticalChance, maxCriticalChance);

        armor = new Stat(agility.GetValue() * armorForAgility);
        evasionChance = new Stat((mastery.GetValue() * evasionForMastery) + agility.GetValue() * evasionForAgility, minEvasionChance, maxEvasionChance);
    }


    public void ChangeDamageType(ContainerDamageTypes type)
    {
        switch (type)
        {
            case ContainerDamageTypes.PhysicalDamage:
                Debug.Log(gameObject.name + " choise the PhysicalDamage!");
                DamageType = new PhysicalDamage(attackDamage.GetValue());
                break;
            case ContainerDamageTypes.FireDamage:
                Debug.Log(gameObject.name + " choise the FireDamage!");
                DamageType = new FireDamage(attackDamage.GetValue());
                break;
            case ContainerDamageTypes.IceDamage:
                Debug.Log(gameObject.name + " choise the IceDamage!");
                DamageType = new IceDamage(attackDamage.GetValue());

                break;
            case ContainerDamageTypes.BleedingDamage:
                Debug.Log(gameObject.name + " choise the BleedingDamage!");
                DamageType = new BleedingDamage(attackDamage.GetValue());
                break;
            case ContainerDamageTypes.PoisonDamage:
                Debug.Log(gameObject.name + " choise the PoisonDamage!");
                DamageType = new PoisonDamage(attackDamage.GetValue());
                break;
            default:
                Debug.LogError(gameObject.name + " : error in Damage Type!");
                break;
        }
    }


    public override float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage)
    {
        //Если вероятность уворотов больше нуля И если рандом говорит о том, что нужно увернуться
        if (evasionChance.GetValue() > 0f && Random.Range(1f, 100f) <= evasionChance.GetValue())
        {
            Debug.Log(transform.name + " dodge the damage!"); //Задоджили урон!
        }
        else //Получаем урон
        {
            damage = base.TakeDamage(killerStats, damageType, damage);
        }

        healthBar.DecreaseHealthBar();

        return damage;
    }


    public void Healing(float amount)
    {
        CurrentHealthPoint += amount;
        if (CurrentHealthPoint > maxHealthPoint.GetValue())
        {
            CurrentHealthPoint = maxHealthPoint.GetValue();
        }

        healthBar.IncreaseHealthBar();
    }


    public virtual void GetExperience(int amount, out bool isLvlUp)
    {
        isLvlUp = false;
        float currentLvl = level.GetLvl();

        //множитель получаемого опыта
        level.AddExperience((int)(amount * (1 + (mastery.GetValue() * experieneMultiplierForMastery))));

        //Если уровень повысился
        if (currentLvl < level.GetLvl())
        {
            isLvlUp = true;
            lvlBar.ShowLvl();
        }
    }

    public override void Die(CharacterStats killerStats)
    {
        if (killerStats != null && killerStats != this)
        {
            killerStats.GetExperience(level.GetAllExperience(), out _); //При смерти отдаем опыт
        }

        Debug.Log(transform.name + " Умер!");
    }

}
