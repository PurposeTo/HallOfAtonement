﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class CharacterStats : UnitStats
{
    private protected float healthPointConcentration;

    public CharacterPresenter CharacterPresenter { get; private protected set; }

    public HealthBar healthBar;
    public LvlBar lvlBar;

    public LevelSystem level = new LevelSystem();
    public Attribute strength = new Attribute();
    public Attribute agility = new Attribute();
    public Attribute mastery = new Attribute();

    private Coroutine coroutinePutAvailableSkillPoints;
    private const int skillPointsPerLevel = 1;
    private int totalAvailableSkillPoints;
    private protected PercentStat chanceToGetAnExtraSkillPoint = new PercentStat();
    private protected Attribute[] allAttributes; // Инициализация в Awake

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
        allAttributes = new Attribute[] { strength, agility, mastery };
    }

    private void OnEnable()
    {
        strength.OnChangeAttributeFinalValue += UpdateBaseStrenghtStatsValue;
        agility.OnChangeAttributeFinalValue += UpdateBaseAgilityStatsValue;
        mastery.OnChangeAttributeFinalValue += UpdateBaseMasteryStatsValue;
        level.OnLevelUp += PutSkillPoints;
    }


    private void OnDisable()
    {
        strength.OnChangeAttributeFinalValue -= UpdateBaseStrenghtStatsValue;
        agility.OnChangeAttributeFinalValue -= UpdateBaseAgilityStatsValue;
        mastery.OnChangeAttributeFinalValue -= UpdateBaseMasteryStatsValue;
        level.OnLevelUp -= PutSkillPoints;
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

        armor = new Stat(BaseArmor + (agility.GetValue() * armorForAgility));
        evasionChance = new PercentStat(agility.GetValue() * evasionForAgility);

        poisonResistance = new PercentStat(basePoisonResistanceValue);
        bleedingResistance = new PercentStat(baseBleedingResistanceValue);
        fireResistance = new PercentStat(baseFireResistanceValue);
        iceResistance = new PercentStat(baseIceResistanceValue);
    }


    private protected virtual void UpdateBaseStrenghtStatsValue()
    {
        Debug.Log("Warning! " + gameObject.name + " changed his BaseStrenghtStats Value!");

        float oldCurrentHealthPercent = CurrentHealthPoint / maxHealthPoint.GetValue();
        maxHealthPoint.ChangeBaseValue(BaseMaxHealthPoint + (strength.GetValue() * hpForStrenght));
        CurrentHealthPoint = oldCurrentHealthPercent * maxHealthPoint.GetValue();

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
        if (canEvade && (isEvaded || (evasionChance.GetValue() > 0f && Random.Range(0f, 1f) <= evasionChance.GetValue())))
        {
            isEvaded = true;
            Debug.Log(transform.name + " dodge the damage!"); //Задоджили урон!
        }
        else //Получаем урон
        {
            if (!isBlocked)
            {
                damage = base.TakeDamage(killerStats, damageType, damage, ref isEvaded, ref isBlocked, canEvade);
            }
            else
            {
                damage = 0f;
            }
        }


        return damage;
    }


    public virtual float Healing(float amount)
    {
        float UnclaimingHealthPoints = 0f;
        float _maxHealthPoint = maxHealthPoint.GetValue();

        CurrentHealthPoint += amount;
        if (CurrentHealthPoint > _maxHealthPoint)
        {
            UnclaimingHealthPoints = CurrentHealthPoint - _maxHealthPoint;
            CurrentHealthPoint = _maxHealthPoint;
        }

        ReportUpdateHealthValue();

        return UnclaimingHealthPoints;
    }


    public virtual void ExtraHealing(float amount) 
    {
        healthPointConcentration = Healing(amount);
    }


    public virtual void GetExperience(int amount)
    {
        // Множитель получаемого опыта
        level.AddExperience((int)(amount * (1 + (mastery.GetValue() * experieneMultiplierForMastery))));

        //AmountOfNewLvls = level.GetLvl() - currentLvl;
        //// Если уровень повысился
        //if (AmountOfNewLvls > 0)
        //{
        //    lvlBar.ShowLvl();

        //    UseSkillPoints(AmountOfNewLvls);
        //}
    }


    private void PutSkillPoints()
    {
        totalAvailableSkillPoints += GetSkillPoints();


        if(coroutinePutAvailableSkillPoints == null) 
        {
            coroutinePutAvailableSkillPoints = StartCoroutine(EnumeratorPutAvailableSkillPoints());
        }
    }


    private IEnumerator EnumeratorPutAvailableSkillPoints()
    {
        while (totalAvailableSkillPoints > 0)
        {
            yield return null;
            RaiseRandomAttribute(); // Повысить атрибут на количество скилл-поинтов, которые доступны
            totalAvailableSkillPoints -= 1;
        }

        coroutinePutAvailableSkillPoints = null;
    }


    private int GetSkillPoints()
    {
        int ExstraSkillPoints = 0;

        // Кэш процентного стата
        float _chanceToGetAnExtraSkillPoint = chanceToGetAnExtraSkillPoint.GetValue();

        if (_chanceToGetAnExtraSkillPoint > 0f && Random.Range(0f, 1f) < _chanceToGetAnExtraSkillPoint)
        {
            Debug.Log("Везунчик! +1 Extra Skill Point!");
            ExstraSkillPoints++;
        }

        int totalAvailableSkillPoints = skillPointsPerLevel + ExstraSkillPoints;
        return totalAvailableSkillPoints;
    }


    private void RaiseRandomAttribute()
    {
        float totalMass = 0; // Общая масса

        for (int i = 0; i < allAttributes.Length; i++) // Вычисление общего шанса-массы всех обьектов
        {
            totalMass += allAttributes[i].GetMassPerAtribute(); // Вычисление общей массы объектов
        }


        float Choise = Random.Range(0, totalMass); // Делаем случайный выбор элемента

        float upperLimit = 0; // Число для проверки диапозона

        for (int i = 0; i < allAttributes.Length; i++)  // Пройтись по всем атрибутам и проверить, какой выпал?
        {
            upperLimit += allAttributes[i].GetMassPerAtribute(); // Вычисление текущего шанса-массы обьекта

            if (Choise <= upperLimit) // Проверяем, это текущий элемент?
            {
                // Это текущий элемент!

                int newAttributeValue = allAttributes[i].GetBaseValue() + 1;
                allAttributes[i].ChangeBaseValue(newAttributeValue);

                Debug.Log(gameObject.name + " повысил уровень! Получите +1 к случайному атрибуту!");

                return;

            } // Если нет, идем проверять дальше    
        }
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
