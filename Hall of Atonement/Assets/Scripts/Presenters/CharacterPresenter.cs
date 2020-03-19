using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
//[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterController))]
public class CharacterPresenter : MonoBehaviour
{
    public CharacterStats MyStats { get; private protected set; }
    public CharacterCombat Combat { get; private protected set; }

    public CharacterController Controller { get; private protected set; }

    public CharacterType CharacterType { get; private set; }


    private protected virtual void Awake()
    {
        MyStats = GetComponent<CharacterStats>();
        Combat = GetComponent<CharacterCombat>();
        Controller = GetComponent<CharacterController>();
        CharacterType = GetComponent<CharacterType>();
    }
}
