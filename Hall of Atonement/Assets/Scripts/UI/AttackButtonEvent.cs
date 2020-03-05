using UnityEngine;
using UnityEngine.EventSystems;

public delegate void PressingAttackButton(bool isAttacking);
public class AttackButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event PressingAttackButton OnPressingAttackButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPressingAttackButton != null) OnPressingAttackButton(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPressingAttackButton != null) OnPressingAttackButton(false);
    }
}
