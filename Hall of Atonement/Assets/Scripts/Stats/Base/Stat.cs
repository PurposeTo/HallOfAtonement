using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;    // Starting value


    public Stat() : this(0f) { }

    public Stat(float baseValue) :this(baseValue, 0f) { }

    public Stat(float baseValue, float minValue) : this(baseValue, minValue, float.MaxValue) { }

    public Stat(float baseValue, float minValue, float maxValue)
    {
        //Стата должна быть только положительной. Так же можно задать минимальное и максимальное значение
        this.baseValue = Mathf.Clamp(baseValue, minValue, maxValue);

    }


    public float GetValue()
    {
        return baseValue;
    }
}
