using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public abstract class CharacterStats : UnitStats
{
    private protected Rigidbody2D rb2D;

    public LevelSystem level = new LevelSystem();
    public Attribute strength = new Attribute();
    public Attribute agility = new Attribute();

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
    private readonly float evasionForAgility = 0.1f;

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

    private readonly float minCriticalMultiplier = 1.1f; //минимальное значение множителя критической атаки
    private protected virtual float BaseCriticalMultiplier { get; } = 2f; //базовое значение множителя критической атаки
    private readonly float minCriticalChance = 0f; //минимальное значение скорости поворот
    private readonly float maxCriticalChance = 100f; //минимальное значение скорости поворот
    private protected virtual float BaseCriticalChance { get; } = 1f; //базовое значение скорости поворот

    private readonly float minEvasionChance = 0f; //минимальное значение скорости уворота
    private readonly float maxEvasionChance = 70f; //минимальное значение скорости уворота
    private float moveEvasion = 0f; //Вероятность уворота при движении


    //public Stat maxHealthPoint;
    public Stat healthPointRegen;
    public Stat movementSpeed;
    public Stat rotationSpeed;
    public readonly float faceEuler = 60f; //Угол лицевой стороны существа. Все действия игрок совершает лицом к объекту действий!
    public Stat attackDamage;
    public Stat attackSpeed; //(Кол-во атак в секунду)
    public Stat criticalMultiplier; //Крит. множитель атаки
    public Stat criticalChance;
    //public Stat armor; //Нет базового значения
    public Stat evasionChance; //Нет базового значения


    private protected override void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        base.Start();
        CurrentHealthPoint = maxHealthPoint.GetValue();
    }


    private protected virtual void FixedUpdate()
    {
        moveEvasion = Mathf.Lerp(0f, 20f, rb2D.velocity.magnitude / maxMovementSpeed);

        CurrentHealthPoint += healthPointRegen.GetValue() * Time.fixedDeltaTime;
        HealthPointClamp();
    }


    //Инициализация статов в зависимости от атрибутов
    private protected override void StatInitialization()
    {
        //Разделить отдельно для ловкости и силы!
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
        criticalMultiplier = new Stat(BaseCriticalMultiplier, minCriticalMultiplier);
        criticalChance = new Stat(BaseCriticalChance, minCriticalChance, maxCriticalChance);

        armor = new Stat(agility.GetValue() * armorForAgility);
        evasionChance = new Stat(agility.GetValue() * evasionForAgility, minEvasionChance, maxEvasionChance);
    }


    public override float TakeDamage(CharacterStats killerStats, float damage)
    {
        //Если вероятность уворотов больше нуля И если рандом говорит о том, что нужно увернуться
        if (evasionChance.GetValue() > 0f && Random.Range(1f, 100f) <= evasionChance.GetValue())
        {
            Debug.Log(transform.name + " dodge the damage!"); //Задоджили урон!
        }
        else //Получаем урон
        {
<<<<<<< Updated upstream
            base.TakeDamage(killerStats, damage);
=======
            //Cнижаем урон броней
            damage = ReduceDamageFromArmor(damage);

            Debug.Log(transform.name + " takes " + damage + " damage.");

            if (CurrentHealthPoint - damage <= 0f) //Если из за полученного урона здоровье будет равно или ниже нуля
            {
                CurrentHealthPoint = 0f;

                Die(killerStats);
            }
            else
            {
                CurrentHealthPoint -= damage;
            }

>>>>>>> Stashed changes
        }
        return damage;
    }


    public virtual void GetExperience(int amount)
    {
        //множитель получаемого опыта
        level.AddExperience(amount);
    }


    public virtual void HealthPointClamp()
    {
        CurrentHealthPoint = Mathf.Clamp(CurrentHealthPoint, 0, maxHealthPoint.GetValue());
    }

    public override void Die(CharacterStats killerStats)
    {
        if (killerStats != null && killerStats != this)
        {
            killerStats.GetExperience(level.GetAllExperience()); //При смерти отдаем опыт
        }

        Debug.Log(transform.name + " Умер!");
    }

}
