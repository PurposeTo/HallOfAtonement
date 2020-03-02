using UnityEngine;

[System.Serializable]
public class LevelSystem
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem() :this(0) { }

    public LevelSystem(int level) :this(level, 0) { }

    public LevelSystem(int level, int experience)
    {
        level = Mathf.Clamp(level, 0, int.MaxValue); //Уровень должен быть только положительным!
        experience = Mathf.Clamp(experience, 0, int.MaxValue); //Опыт должен быть только положительным!

        this.level = level;
        this.experience = experience;
        experienceToNextLevel = CalculateExperienceToNextLevel(level);
    }


    public void AddExperience(int amount)
    {
        experience += amount;
        while(experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel; //Сначала вычесть, так как experienceToNextLevel зависит от уровня!
            level++;
            experienceToNextLevel = CalculateExperienceToNextLevel(level);
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
