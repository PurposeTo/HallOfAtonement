using UnityEngine;

public class PlayerUIPresenter : MonoBehaviour
{
    // Не путать! PlayerUIPresenter и CharacterUIPresenter разные вещи!
    public Joystick PlayerJoystick;
    public AttackButtonEvent PlayerAttackButton;
    public PlayerStatusBar StatusBar;
}
