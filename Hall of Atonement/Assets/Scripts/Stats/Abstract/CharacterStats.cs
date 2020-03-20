using UnityEngine;
using System.Collections.Generic;

public abstract class CharacterStats : UnitStats
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }

    public HealthBar healthBar;
    public LvlBar lvlBar;

    public LevelSystem level = new LevelSystem();
    public Attribute strength = new Attribute();
    public Attribute agility = new Attribute();
    public Attribute mastery = new Attribute();

    //Зависимость статов от Силы
    private readonly float hpForStrenght = 20f;
    private readonly float attackDamageForStrenght = 3f;
    private readonly float attackSpeedForStrenght = -0.015f;
    private readonly float movementSpeedForStrenght = -0.03f;
    private readonly float rotationSpeedForStrenght = -4f;


    //Зависимость статов от Ловковсти
    private readonly float armorForAgility = 0.5f;
    private readonly float evasionForAgility = 0.005f;
    private readonly float attackDamageForAgility = 0.75f;
    private readonly float attackSpeedForAgility = 0.3f;
    private readonly float movementSpeedForAgility = 0.04f;
    private readonly float rotationSpeedForAgility = 5f;


    //Зависимость статов от Мастерства
    private readonly float criticalChanceForMastery = 0.02f;
    private readonly float criticalMultiplierForMastery = 0.1f;
    private readonly float experieneMultiplierForMastery = 0.1f;
    //private readonly float улучшение баффов


    private protected override float BaseMaxHealthPoint { get; } = 100f; //базовое значение максимального кол-ва здоровья
    //public float CurrentHealthPoint { get; private protected set; }

    private const float minMovementSpeed = 1.6f; //минимальное значение скорости
    private const float maxMovementSpeed = 20f; //максимальное значение скорости
    private protected virtual float BaseMovementSpeed { get; } = 7f; //базовое значение скорости

    private const float minRotationSpeed = 180f; //минимальное значение скорости поворота
    private protected virtual float BaseRotationSpeed { get; } = 720f; //базовое значение скорости поворота //Соотносится как ~ 1080 к 10 скорости

    public virtual float BaseAttackDamage { get; } = 10f; //базовое значение атаки
    private const float minAttackSpeed = 0.01f; //максимальное значение скорости атаки
    private const float maxAttackSpeed = 50f; //максимальное значение скорости атаки
    private protected virtual float BaseAttackSpeed { get; } = 0.75f; //базовое значение скорости атаки

    private const float minCriticalMultiplier = 1.1f; //минимальное значение множителя критической атаки
    private protected virtual float BaseCriticalMultiplier { get; } = 2f; //базовое значение множителя критической атаки.
    private protected virtual float BaseCriticalChance { get; } = 0.01f; //базовое значение скорости поворот

    //public Stat maxHealthPoint;
    public Stat movementSpeed;
    public Stat rotationSpeed;
    public readonly float faceEuler = 60f; //Угол лицевой стороны существа. Все действия игрок совершает лицом к объекту действий!
    public Stat attackDamage;

    public enum ContainerDamageTypes
    {
        PhysicalDamage,
        FireDamage,
        IceDamage
    }

    public ContainerDamageTypes UnitDamageType;

    public virtual DamageType DamageType { get; private protected set; }

    public Stat attackSpeed; //(Кол-во атак в секунду)
    public Stat criticalMultiplier; //Крит. множитель атаки
    public PercentStat criticalChance;
    //public Stat armor; //Нет базового значения
    public PercentStat evasionChance; //Нет базового значения


    public List<IDefenseModifier> defenseModifiers = new List<IDefenseModifier>();


    private protected override void Awake()
    {
        base.Awake();
        ChangeDamageType(UnitDamageType);
    }

    private void OnEnable()
    {
        strength.OnChangeAttributeFinalValue += UpdateBaseStrenghtStatsValue;
        agility.OnChangeAttributeFinalValue += UpdateBaseAgilityStatsValue;
        mastery.OnChangeAttributeFinalValue += UpdateBaseMasteryStatsValue;
    }


    private void OnDisable()
    {
        strength.OnChangeAttributeFinalValue -= UpdateBaseStrenghtStatsValue;
        agility.OnChangeAttributeFinalValue -= UpdateBaseAgilityStatsValue;
        mastery.OnChangeAttributeFinalValue -= UpdateBaseMasteryStatsValue;
    }


    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();
    }


    //Инициализация статов в зависимости от атрибутов
    private protected override void StatInitialization()
    {
        //Разделить отдельно для ловкости, силы и мастерства!
        maxHealthPoint = new Stat(BaseMaxHealthPoint + (strength.GetValue() * hpForStrenght));

        movementSpeed = new Stat(BaseMovementSpeed +
            (strength.GetValue() * movementSpeedForStrenght) + (agility.GetValue() * movementSpeedForAgility), minMovementSpeed, maxMovementSpeed);
        rotationSpeed = new Stat(BaseRotationSpeed +
            (strength.GetValue() * rotationSpeedForStrenght) + (agility.GetValue() * rotationSpeedForAgility), minRotationSpeed);

        attackDamage = new Stat(BaseAttackDamage +
            (strength.GetValue() * attackDamageForStrenght) + (agility.GetValue() * attackDamageForAgility));

        attackSpeed = new Stat(BaseAttackSpeed +
            (strength.GetValue() * attackSpeedForStrenght) + (agility.GetValue() * attackSpeedForAgility), minAttackSpeed, maxAttackSpeed);
        criticalMultiplier = new Stat(BaseCriticalMultiplier + (mastery.GetValue() * criticalMultiplierForMastery), minCriticalMultiplier);
        criticalChance = new PercentStat(BaseCriticalChance + (mastery.GetValue() * criticalChanceForMastery));

        armor = new Stat(agility.GetValue() * armorForAgility);
        evasionChance = new PercentStat(agility.GetValue() * evasionForAgility);

        poisonResistance = new PercentStat(basePoisonResistanceValue);
        bleedingResistance = new PercentStat(baseBleedingResistanceValue);
        fireResistance = new PercentStat(baseFireResistanceValue);
        iceResistance = new PercentStat(baseIceResistanceValue);
    }


    private protected virtual void UpdateBaseStrenghtStatsValue()
    {
        Debug.Log("Warning! " + gameObject.name + " changed his BaseStrenghtStats Value!");

        maxHealthPoint.ChangeBaseValue(BaseMaxHealthPoint + (strength.GetValue() * hpForStrenght));

        movementSpeed.ChangeBaseValue(BaseMovementSpeed + (strength.GetValue() * movementSpeedForStrenght) + (agility.GetValue() * movementSpeedForAgility));
        rotationSpeed.ChangeBaseValue(BaseRotationSpeed + (strength.GetValue() * rotationSpeedForStrenght) + (agility.GetValue() * rotationSpeedForAgility));

        attackDamage.ChangeBaseValue(BaseAttackDamage + (strength.GetValue() * attackDamageForStrenght) + (agility.GetValue() * attackDamageForAgility));

        attackSpeed.ChangeBaseValue(BaseAttackSpeed + (strength.GetValue() * attackSpeedForStrenght) + (agility.GetValue() * attackSpeedForAgility));

    }


    private protected virtual void UpdateBaseAgilityStatsValue()
    {
        Debug.Log("Warning! " + gameObject.name + " changed his BaseAgilityStats Value!");

        movementSpeed.ChangeBaseValue(BaseMovementSpeed + (strength.GetValue() * movementSpeedForStrenght) + (agility.GetValue() * movementSpeedForAgility));
        rotationSpeed.ChangeBaseValue(BaseRotationSpeed + (strength.GetValue() * rotationSpeedForStrenght) + (agility.GetValue() * rotationSpeedForAgility));

        attackDamage.ChangeBaseValue(BaseAttackDamage + (strength.GetValue() * attackDamageForStrenght) + (agility.GetValue() * attackDamageForAgility));

        attackSpeed.ChangeBaseValue(BaseAttackSpeed + (strength.GetValue() * attackSpeedForStrenght) + (agility.GetValue() * attackSpeedForAgility));

        armor.ChangeBaseValue(agility.GetValue() * armorForAgility);
        evasionChance.ChangeBaseValue(agility.GetValue() * evasionForAgility);
    }


    private protected virtual void UpdateBaseMasteryStatsValue()
    {
        Debug.Log("Warning! " + gameObject.name + " changed his BaseMasteryStats Value!");

        criticalMultiplier.ChangeBaseValue(BaseCriticalMultiplier + (mastery.GetValue() * criticalMultiplierForMastery));
        criticalChance.ChangeBaseValue(BaseCriticalChance + (mastery.GetValue() * criticalChanceForMastery));
    }


    public void ChangeDamageType(ContainerDamageTypes type)
    {
        switch (type)
        {
            case ContainerDamageTypes.PhysicalDamage:
                Debug.Log(gameObject.name + " choise the PhysicalDamage!");
                DamageType = new PhysicalDamage();
                break;
            case ContainerDamageTypes.FireDamage:
                Debug.Log(gameObject.name + " choise the FireDamage!");
                DamageType = new FireDamage();
                break;
            case ContainerDamageTypes.IceDamage:
                Debug.Log(gameObject.name + " choise the IceDamage!");
                DamageType = new IceDamage();
                break;
            default:
                Debug.LogError(gameObject.name + " : error in Damage Type!");
                break;

                // Нельзя выбрать данные типы урона в качестве атаки!
                //case ContainerDamageTypes.BleedingDamage:
                //    Debug.Log(gameObject.name + " choise the BleedingDamage!");
                //    DamageType = new BleedingDamage();
                //    break;
                //case ContainerDamageTypes.PoisonDamage:
                //    Debug.Log(gameObject.name + " choise the PoisonDamage!");
                //    DamageType = new PoisonDamage();
                //    break;
        }
    }


    public override float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked, bool canEvade = true)
    {

        for (int i = 0; i < defenseModifiers.Count; i++)
        {
            defenseModifiers[i].ApplyDefenseModifier(killerStats, damageType, damage, ref isEvaded, ref isBlocked);
        }

        //Если вероятность уворотов больше нуля И если рандом говорит о том, что нужно увернуться
        if (canEvade && (isEvaded || (evasionChance.GetValue() > 0f && Random.Range(1f, 100f) <= evasionChance.GetValue())))
        {
            isEvaded = true;
            Debug.Log(transform.name + " dodge the damage!"); //Задоджили урон!
        }
        else //Получаем урон
        {
            damage = base.TakeDamage(killerStats, damageType, damage, ref isEvaded, ref isBlocked, canEvade);

            if (!isBlocked)
            {
                healthBar.DecreaseHealthBar();
            }
        }


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
