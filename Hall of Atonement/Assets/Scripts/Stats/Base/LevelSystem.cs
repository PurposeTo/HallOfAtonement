using UnityEngine;

public delegate void LevelUp();

[System.Serializable]
public class LevelSystem
{
    public LevelSystem()
    {
        // Нельзя задавать в конструкторе начальные значения уровня и атрибутов, так как нельзя будет сразу вложить их в атрибуты, так как те не инициализированны!

        experienceToNextLevel = CalculateExperienceToNextLevel(level);
        experience = experienceToNextLevel / 2; // У всех по дефолту уже есть часть опыта\

    }


    //public LevelSystem() :this(0) { }

    //public LevelSystem(int level) :this(level, 0) { }

    //public LevelSystem(int level, int experience)
    //{
    //    level = Mathf.Clamp(level, 0, int.MaxValue); //Уровень должен быть только положительным!
    //    experience = Mathf.Clamp(experience, 0, int.MaxValue); //Опыт должен быть только положительным!

    //    this.level = level;
    //    this.experience = experience;
    //    experienceToNextLevel = CalculateExperienceToNextLevel(level);
    //}
    

    private int level;
    private int experience;
    private int experienceToNextLevel;

    public event LevelUp OnLevelUp;

    public void InitializingLevel(int level)
    {
        if (this.level == 0)
        {
            this.level = level;

            experienceToNextLevel = CalculateExperienceToNextLevel(level);

            for (int i = 0; i < level; i++)
            {
                OnLevelUp?.Invoke();
            }
        }
        else
        {
            Debug.LogError("Try to attempt to ReInitialize");
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        while(experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel; //Сначала вычесть, так как experienceToNextLevel зависит от уровня!
            level++;
            experienceToNextLevel = CalculateExperienceToNextLevel(level);

            OnLevelUp?.Invoke();
        }
    }


    public int GetAllExperience() //Функция возвращает число всего накопленного опыта
    {
        int lvlCount = level;
        int allExp = experience;
        while (lvlCount > 0)
        {
            allExp += lvlCount * 10;
            lvlCount--;
        }
        return allExp;
    }


    public int GetLvl()
    {
        return level;
    }


    private int CalculateExperienceToNextLevel(int level)
    {
        return (level + 1) * 10;
    }
}
