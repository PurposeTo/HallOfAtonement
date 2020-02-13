using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class StatusBar : MonoBehaviour
{
    private CharacterStats myStats;


    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();

        //тест
        PoisonEffect poison = new PoisonEffect();
        statusEffects.Add(poison);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < statusEffects.Count; i++)
        {
            statusEffects[i].Action(myStats);
        }
    }
}
