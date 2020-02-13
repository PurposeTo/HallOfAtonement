using UnityEngine;

[System.Serializable]
public class Attribute
{
    [SerializeField]
    private int baseValue;    // Starting value


    public Attribute() : this(0) { }

    public Attribute(int baseValue)
    {
        this.baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue); //Атрибут должен быть только положительный!
    }


    public int GetValue()
    {
        return baseValue;
    }
}
